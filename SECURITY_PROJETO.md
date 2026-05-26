# SECURITY_PROJETO — Analise e Plano de Segurança

> **Projeto:** SistemaPDV — BackendApiWEB (ASP.NET Core)
> **Data da analise:** 2026-05-25
> **Status geral:** CRITICO — Nao apto para producao sem correcoes

---

## Indice

1. [Resumo Executivo](#1-resumo-executivo)
2. [Vulnerabilidades Criticas](#2-vulnerabilidades-criticas)
3. [Vulnerabilidades Altas](#3-vulnerabilidades-altas)
4. [Vulnerabilidades Medias](#4-vulnerabilidades-medias)
5. [Vulnerabilidades Baixas](#5-vulnerabilidades-baixas)
6. [O que esta correto](#6-o-que-esta-correto)
7. [Plano de Correcao Priorizado](#7-plano-de-correcao-priorizado)
8. [Exemplos de Codigo para Correcao](#8-exemplos-de-codigo-para-correcao)

---

## 1. Resumo Executivo

A analise de segurança identificou **17 vulnerabilidades** distribuidas em 4 niveis de severidade. O sistema possui credenciais expostas em codigo-fonte, ausencia total de autenticacao/autorizacao aplicada, HTTPS desabilitado e sem protecao contra ataques basicos como brute-force e enumeracao de usuarios.

| Severidade | Quantidade |
|------------|-----------|
| CRITICO    | 5         |
| ALTO       | 3         |
| MEDIO      | 6         |
| BAIXO      | 3         |

---

## 2. Vulnerabilidades Criticas

### C1 — Credenciais hardcoded no codigo-fonte

**Arquivo:** `Service/Implementations/EmailService.cs` (linhas 9–10)

```csharp
// PROBLEMA — nunca faca isso
var remetente = "gilsonfonseca3000@gmail.com";
var senha = "djvjclgcxeqlwbgy";
```

**Risco:** Qualquer pessoa com acesso ao repositorio pode comprometer a conta de e-mail.

**Correcao:** Usar `dotnet user-secrets` em desenvolvimento e variaveis de ambiente em producao (ver secao 8.1).

---

### C2 — Senha do banco de dados exposta e trivial

**Arquivo:** `appsettings.json` (linha 3)

```json
// PROBLEMA
"DefaultConnection": "Server=localhost;Database=SistemaPDV;User Id=sa;Password=123456;"
```

**Risco:** Acesso total ao banco de dados com usuario SA (administrador).

**Correcao:** Mover connection string para variaveis de ambiente. Nunca usar conta SA em aplicacoes.

---

### C3 — HTTPS desativado

**Arquivo:** `Program.cs` (linha 68)

```csharp
// PROBLEMA — linha comentada
// app.UseHttpsRedirection();
```

**Risco:** Todo o trafego (tokens, senhas, dados) trafega em texto puro, vulneravel a interceptacao.

**Correcao:** Descomentar a linha e adicionar HSTS (ver secao 8.2).

---

### C4 — Autenticacao nao aplicada no middleware

**Arquivo:** `Program.cs`

```csharp
// PROBLEMA — nenhuma dessas linhas existe no Program.cs
// builder.Services.AddAuthentication().AddJwtBearer(...)
// app.UseAuthentication();
// app.UseAuthorization();
```

A chave JWT existe no `appsettings.json` mas nunca e usada. O pipeline nao valida tokens.

**Risco:** Qualquer pessoa pode chamar qualquer endpoint sem autenticacao.

**Correcao:** Registrar e configurar JWT no pipeline (ver secao 8.3).

---

### C5 — Nenhum endpoint protegido com [Authorize]

**Arquivos afetados:**

| Controller | Risco |
|------------|-------|
| `UserController.cs` | Leitura publica de todos os usuarios |
| `AuthController.cs` | Update/Delete sem autenticacao |
| `ProdutosController.cs` | CRUD publico de produtos |
| `ResetSenhaController.cs` | Reset de senha sem protecao |

**Correcao:** Adicionar `[Authorize]` nos controllers e metodos sensiveis (ver secao 8.4).

---

## 3. Vulnerabilidades Altas

### A1 — Chave JWT fraca e exposta

**Arquivo:** `appsettings.json` (linha 8)

```json
// PROBLEMA — chave fraca e visivel no repositorio
"Key": "uma-chave-super-segura-de-no-minimo-32-caracteres"
```

**Risco:** Token JWT pode ser forjado se a chave for descoberta.

**Correcao:** Gerar chave com 64+ caracteres aleatorios e armazenar em segredo (ver secao 8.1).

---

### A2 — Sem protecao CSRF

**Arquivo:** `Program.cs` (linhas 40–50)

```csharp
// PROBLEMA — AllowCredentials sem CSRF mitigation
policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
```

**Risco:** Ataques CSRF em operacoes de escrita (POST, PUT, DELETE).

**Correcao:** Implementar `ValidateAntiForgeryToken` ou garantir uso de tokens Bearer (nao cookies de sessao).

---

### A3 — DTOs sem validacao de entrada

**Arquivos afetados:** `DTOs/LoginRequest.cs`, `DTOs/UpdateUserRequest.cs`, `DTOs/ProdutoCreateRequest.cs`, `DTOs/ResetSenhaRequest.cs`

```csharp
// PROBLEMA — sem nenhuma anotacao de validacao
public class LoginRequest {
    public string Email { get; set; }
    public string Senha { get; set; }
}
```

**Risco:** Entradas invalidas ou maliciosas podem causar erros inesperados ou bypassar regras de negocio.

**Correcao:** Adicionar `[Required]`, `[EmailAddress]`, `[StringLength]`, `[Range]` (ver secao 8.5).

---

## 4. Vulnerabilidades Medias

### M1 — Enumeracao de usuarios no login

**Arquivo:** `Service/Implementations/AuthService.cs` (linhas 34–35)

```csharp
// PROBLEMA — mensagens diferentes revelam se o e-mail existe
if (usuario == null)
    return new AuthResult(false, "Usuário não encontrado.", null);
if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
    return new AuthResult(false, "Senha incorreta.", null);
```

**Correcao:**

```csharp
// CORRETO — mensagem generica para ambos os casos
return new AuthResult(false, "E-mail ou senha inválidos.", null);
```

---

### M2 — Enumeracao de usuarios no reset de senha

**Arquivo:** `Service/Implementations/AuthService.cs` (linha 151)

```csharp
// PROBLEMA
if (usuario == null)
    return new AuthResult(false, "E-mail não encontrado.", null);
```

**Correcao:**

```csharp
// CORRETO — nao revela se o e-mail existe
if (usuario == null)
    return new AuthResult(true, "Se o e-mail existir, um link será enviado.", null);
```

---

### M3 — Requisitos de senha fracos

**Arquivo:** `Service/Implementations/AuthService.cs` (linha 70)

```csharp
// PROBLEMA — minimo de 6 caracteres, sem complexidade
if (dto.Senha.Length < 6)
    return new AuthResult(false, "A senha deve ter no mínimo 6 caracteres.", null);
```

**Correcao:**

```csharp
// CORRETO — minimo 12 caracteres com complexidade
var senhaRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{12,}$");
if (!senhaRegex.IsMatch(dto.Senha))
    return new AuthResult(false, "A senha deve ter no mínimo 12 caracteres, incluindo maiúsculas, minúsculas, números e símbolos.", null);
```

---

### M4 — Endpoint de teste expoe erros internos

**Arquivo:** `Controllers/TesteController.cs` (linha 21)

```csharp
// PROBLEMA — retorna mensagem raw da excecao
catch (Exception ex) {
    return StatusCode(500, ex.Message); // vaza detalhes do banco
}
```

**Correcao:** Remover o endpoint em producao ou retornar mensagem generica:

```csharp
catch (Exception) {
    return StatusCode(500, "Erro interno do servidor.");
}
```

---

### M5 — URL de reset de senha usa HTTP e token na query string

**Arquivo:** `Service/Implementations/AuthService.cs` (linha 159)

```csharp
// PROBLEMA — HTTP + token visivel no historico do browser
var link = $"http://localhost:4200/redefinirsenha?token={token}";
```

**Correcao:**

```csharp
// CORRETO — HTTPS + configuravel por ambiente
var baseUrl = _configuration["App:FrontendUrl"]; // ex: https://meusite.com
var link = $"{baseUrl}/redefinirsenha?token={token}";
```

---

### M6 — CORS permissivo demais

**Arquivo:** `Program.cs` (linhas 46–47)

```csharp
// PROBLEMA
.AllowAnyHeader()
.AllowAnyMethod()
```

**Correcao:**

```csharp
// CORRETO — restringir metodos e headers necessarios
policy
    .WithOrigins("https://meusite.com")
    .WithMethods("GET", "POST", "PUT", "DELETE")
    .WithHeaders("Authorization", "Content-Type");
```

---

## 5. Vulnerabilidades Baixas

### B1 — Sem rate limiting nos endpoints de autenticacao

**Risco:** Ataques de brute-force em `/auth/login`, `/auth/registrar`, `/resetsenha`.

**Correcao:** Instalar pacote `AspNetCoreRateLimit` e configurar limites (ver secao 8.6).

---

### B2 — Sem HSTS (HTTP Strict Transport Security)

**Correcao:** Adicionar em `Program.cs`:

```csharp
app.UseHsts();
```

---

### B3 — Swagger pode vazar em producao

**Arquivo:** `Program.cs` (linhas 62–66)

A interface Swagger esta protegida pelo ambiente, mas o JSON `/swagger/v1/swagger.json` pode ainda estar acessivel. Confirmar que toda a configuracao Swagger esta dentro do bloco `if (app.Environment.IsDevelopment())`.

---

## 6. O que esta correto

| Item | Arquivo |
|------|---------|
| Senhas hasheadas com BCrypt | `AuthService.cs:81` |
| Swagger restrito ao ambiente de desenvolvimento | `Program.cs:62` |
| Injecao de dependencia estruturada | `Program.cs` |

---

## 7. Plano de Correcao Priorizado

| Prioridade | Tarefa | Severidade |
|-----------|--------|-----------|
| 1 | Mover credenciais para User Secrets / variaveis de ambiente | CRITICO |
| 2 | Habilitar HTTPS + HSTS | CRITICO |
| 3 | Registrar JWT e aplicar UseAuthentication/UseAuthorization | CRITICO |
| 4 | Adicionar [Authorize] nos controllers | CRITICO |
| 5 | Adicionar validacoes nos DTOs | ALTO |
| 6 | Corrigir mensagens de enumeracao de usuario | MEDIO |
| 7 | Aumentar requisito de senha para 12+ caracteres | MEDIO |
| 8 | Corrigir URL de reset (HTTPS, configuravel) | MEDIO |
| 9 | Restringir CORS | MEDIO |
| 10 | Implementar rate limiting | BAIXO |
| 11 | Remover endpoint de teste ou sanitizar erros | MEDIO |

---

## 8. Exemplos de Codigo para Correcao

### 8.1 — User Secrets (desenvolvimento)

```bash
# No terminal, dentro da pasta do projeto
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Password=SENHA_REAL"
dotnet user-secrets set "Email:Senha" "SENHA_APP_GMAIL"
dotnet user-secrets set "Jwt:Key" "CHAVE_64_CARACTERES_ALEATORIA"
```

No `Program.cs`, o `builder.Configuration` ja le os secrets automaticamente em desenvolvimento.

---

### 8.2 — HTTPS e HSTS

```csharp
// Program.cs
app.UseHttpsRedirection(); // descomente esta linha
app.UseHsts();             // adicione esta linha
```

---

### 8.3 — JWT Authentication no Pipeline

```csharp
// Program.cs — em builder.Services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Program.cs — no pipeline (ordem importa)
app.UseAuthentication();
app.UseAuthorization();
```

---

### 8.4 — Protegendo Controllers

```csharp
// Protege o controller inteiro
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProdutosController : ControllerBase { ... }

// Protege apenas rotas sensiveis
[HttpDelete("{id}")]
[Authorize(Roles = "Admin")]
public IActionResult Delete(Guid id) { ... }

// Libera rota publica dentro de controller protegido
[HttpGet]
[AllowAnonymous]
public IActionResult Listar() { ... }
```

---

### 8.5 — Validacao nos DTOs

```csharp
public class LoginRequest
{
    [Required(ErrorMessage = "E-mail obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Senha obrigatória.")]
    [MinLength(12, ErrorMessage = "Senha deve ter no mínimo 12 caracteres.")]
    public string Senha { get; set; } = "";
}

public class ProdutoCreateRequest
{
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Nome { get; set; } = "";

    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo.")]
    public int Estoque { get; set; }
}
```

---

### 8.6 — Rate Limiting (ASP.NET Core 7+)

```csharp
// Program.cs — builder.Services
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("login", cfg =>
    {
        cfg.PermitLimit = 5;
        cfg.Window = TimeSpan.FromMinutes(1);
        cfg.QueueLimit = 0;
    });
});

// Program.cs — pipeline
app.UseRateLimiter();

// AuthController.cs
[HttpPost("login")]
[EnableRateLimiting("login")]
public IActionResult Login([FromBody] LoginRequest dto) { ... }
```

---

*Documento gerado em 2026-05-25 — Revisitar apos cada ciclo de desenvolvimento.*
