# 📁 BackendApiWEB

> API RESTful desenvolvida em .NET com arquitetura em camadas, utilizando Dapper para acesso a dados e SQL Server como banco de dados.

---

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Pré-requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Uso](#uso)
- [Endpoints da API](#endpoints-da-api)
- [Frontend](#frontend)
- [Contribuindo](#contribuindo)
- [Licença](#licença)

---

## 🎯 Sobre o Projeto

BackendApiWEB é uma API backend robusta construída seguindo os princípios de Clean Architecture e SOLID. O projeto implementa autenticação JWT, controle de permissões e operações CRUD completas para gerenciamento de usuários.

### Principais Funcionalidades

- ✅ Autenticação e Autorização com JWT
- ✅ Gerenciamento de Usuários
- ✅ Sistema de Permissões
- ✅ Paginação de Resultados
- ✅ Validação de Dados
- ✅ Documentação Swagger/OpenAPI

---

## 🏗️ Estrutura do Projeto

### Explicação completa de cada pasta e arquivo

```
BackendApiWEB/
│
├── 📦 Controllers/          # Endpoints da API
│   ├── AuthController.cs        # Login e registro de usuário
│   ├── TesteController.cs       # Testes simples
│   ├── UserController.cs        # CRUD de usuários, paginação
│   └── WeatherForecastController.cs  # Template padrão .NET (pode excluir)
│
├── 📦 Data/                 # Acesso ao Banco de Dados
│   ├── DbContextDapper.cs       # Configuração de conexão com Dapper
│   ├── 📁 interfaces/           # Interfaces dos repositórios
│   │   ├── IUserRepository.cs
│   │   ├── IPermissaoRepository.cs
│   │   └── ...
│   └── 📁 Repositories/         # Implementações dos repositórios
│       ├── UserRepository.cs
│       ├── PermissaoRepository.cs
│       └── ...
│
├── 📦 DataBase/             # Scripts e utilitários de banco
│
├── 📦 DTOs/                 # Objetos de Transferência de Dados
│   ├── AuthResult.cs            # Retorno do login
│   ├── LoginDTO.cs              # Dados de login
│   ├── LoginRequest.cs          # Requisição de login
│   ├── PaginatedResult.cs       # Modelo de paginação
│   ├── RegistrarRequest.cs      # Dados de registro
│   ├── UserResponse.cs          # Resposta de usuário
│   └── UsuarioCreateDTO.cs      # Criação de usuário
│
├── 📦 Models/               # Entidades do Domínio
│   ├── Usuario.cs               # Entidade de usuário
│   ├── Permissao.cs             # Entidade de permissão
│   └── UsuarioPermissao.cs      # Relacionamento usuário-permissão
│
├── 📦 Service/              # Lógica de Negócio
│   ├── 📁 Implementations/      # Implementações dos serviços
│   │   ├── AuthService.cs           # Validação, JWT, autenticação
│   │   ├── UserService.cs           # Regras de negócio de usuários
│   │   └── PermissaoService.cs      # Regras de permissões
│   └── 📁 Interfaces/           # Contratos dos serviços
│       ├── IAuthService.cs
│       ├── IUserService.cs
│       └── IPermissaoService.cs
│
├── 📄 appsettings.json      # Configurações da aplicação
├── 📄 BackendApiWEB.http    # Testes de requisições HTTP
└── 📄 Program.cs            # Ponto de entrada da aplicação
```

---

## 📚 Detalhamento das Camadas

### 📦 Controllers/
**Responsabilidade:** Porta de entrada da API

É onde ficam os endpoints da API. Cada controller recebe requisições HTTP, chama os serviços e devolve respostas.

- **AuthController.cs** → Controla login e registro de usuário
- **TesteController.cs** → Usado para testes simples
- **UserController.cs** → Lista usuários, busca por ID, paginação etc.
- **WeatherForecastController.cs** → Arquivo padrão do template do .NET (pode excluir)

**Resumo:**
> ➡️ **Controladores** são a "Porta de entrada" da API. Nada de regra de negócio aqui.

---

### 📦 Data/
**Responsabilidade:** Acesso ao banco de dados

Contém tudo relacionado ao acesso ao banco.

#### 📄 DbContextDapper.cs
Classe que configura sua conexão com SQL Server usando Dapper.

#### 📁 interfaces/
Interfaces dos repositórios — definem os métodos sem implementação.

**Exemplos:** `IUserRepository`, `IPermissaoRepository`, etc.

#### 📁 Repositories/
Implementações reais que executam SQL no banco usando Dapper.

**Resumo:**
> ➡️ **Repositórios** acessam o banco.
> 
> ➡️ **Controllers** nunca falam direto com o banco — sempre via services que usam repositórios.

---

### 📦 DataBase/
**Responsabilidade:** Scripts e utilitários do banco

Provavelmente contém arquivos utilitários, scripts ou classes auxiliares para banco de dados.

Se estiver vazio, pode ser para organização futura.

---

### 📦 DTOs/
**Responsabilidade:** Contratos de entrada e saída da API

Contém todos os objetos utilizados para entrada e saída da API (payloads).

**Exemplos:**
- **AuthResult.cs** → Retorno do login
- **LoginDTO / LoginRequest.cs** → Dados enviados no login
- **PaginatedResult.cs** → Modelo de paginação
- **RegistrarRequest.cs** → Dados de criação de usuário
- **UserResponse / UsuarioCreateDTO.cs** → Respostas do usuário

**Resumo:**
> ➡️ **DTO** = o que chega na API e o que sai da API.
> 
> ➡️ Nunca expor entidades do banco diretamente.

---

### 📦 Models/
**Responsabilidade:** Representação do domínio

São suas entidades do domínio, que representam tabelas do banco.

- **Permissao.cs** → Entidade de permissão
- **Usuario.cs** → Entidade do usuário
- **UsuarioPermissao.cs** → Relacionamento entre usuário e permissão

**Resumo:**
> ➡️ **Models** refletem tabelas do banco.
> 
> ➡️ **DTOs** refletem dados enviados/recebidos pela API.

---

### 📦 Service/
**Responsabilidade:** Lógica de negócio

Contém a lógica de negócio da aplicação.

#### 📁 Implementations/
Implementações concretas da regra de negócio.

**Exemplos:**
- **AuthService.cs** → Valida credenciais, gera JWT, chama repositório
- **UserService.cs** → Regras de CRUD de usuário
- **PermissaoService.cs** → Regras sobre permissões

#### 📁 Interfaces/
As assinaturas das classes de serviço:
- `IAuthService.cs`
- `IUserService.cs`
- `IPermissaoService.cs`

**Resumo:**
> ➡️ **Services** = regras de negócio.
> 
> ➡️ Regras ficam aqui (não nos controllers e não no repositório).

---

### 📄 appsettings.json
**Responsabilidade:** Configurações da aplicação

Arquivo de configuração geral:
- String de conexão com o banco
- JWT Secret
- Configurações de logs, etc.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;User Id=...;Password=..."
  },
  "JwtSettings": {
    "Secret": "sua-chave-secreta-super-segura",
    "Issuer": "BackendApiWEB",
    "Audience": "FrontendApp",
    "ExpirationInMinutes": 60
  }
}
```

---

### 📄 BackendApiWEB.http
**Responsabilidade:** Testes de API

Arquivo para testar requisições dentro do VS Code / Visual Studio.

---

### 📄 Program.cs
**Responsabilidade:** Inicialização da aplicação

Ponto inicial da aplicação.

**Configura:**
- Injeção de dependências
- Middlewares
- CORS
- Swagger
- Roteamento
- Inicialização da API

---

## 🚀 Tecnologias Utilizadas

- **[.NET 8.0](https://dotnet.microsoft.com/)** - Framework principal
- **[Dapper](https://github.com/DapperLib/Dapper)** - Micro ORM para acesso a dados
- **[SQL Server](https://www.microsoft.com/sql-server)** - Banco de dados
- **[JWT](https://jwt.io/)** - Autenticação e autorização
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentação da API
- **[BCrypt](https://github.com/BcryptNet/bcrypt.net)** - Hash de senhas

---

## 📋 Pré-requisitos

Antes de começar, você precisa ter instalado:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/sql-server/sql-server-downloads) ou [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-editions-express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

---

## 🔧 Instalação

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/BackendApiWEB.git
cd BackendApiWEB
```

### 2. Restaure as dependências

```bash
dotnet restore
```

### 3. Configure o banco de dados

Execute os scripts SQL localizados na pasta `DataBase/` para criar as tabelas necessárias.

### 4. Configure suas credenciais locais (OBRIGATORIO)

> **O arquivo `appsettings.json` NAO esta no repositorio por seguranca.**
> Use o modelo abaixo para criar o seu localmente.

**Passo 1** — Copie o arquivo de exemplo:

```bash
cp BackendApiWEB/appsettings.Example.json BackendApiWEB/appsettings.json
```

**Passo 2** — Abra o `appsettings.json` e preencha com seus dados reais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaPDV;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "GERE_UMA_CHAVE_DE_64_CARACTERES_ALEATORIOS_AQUI",
    "Issuer": "BackendApiWEB",
    "Audience": "FrontendAngular"
  },
  "Email": {
    "Remetente": "seu_email@gmail.com",
    "Senha": "sua_senha_de_app_gmail"
  }
}
```

**Como gerar uma chave JWT segura:**

```bash
# PowerShell
[System.Convert]::ToBase64String((1..64 | ForEach-Object { Get-Random -Maximum 256 }))

# Ou acesse: https://generate-secret.vercel.app/64
```

> **Nunca commite o `appsettings.json` com dados reais. Ele ja esta no `.gitignore`.**

---

## ⚙️ Configuração

### Variáveis de Ambiente (Opcional)

Para produção, é recomendado usar variáveis de ambiente:

```bash
export ConnectionStrings__DefaultConnection="Server=..."
export JwtSettings__Secret="..."
```

### Injeção de Dependências

O projeto utiliza injeção de dependências nativa do .NET. As configurações estão em `Program.cs`:

```csharp
// Repositórios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPermissaoRepository, PermissaoRepository>();

// Serviços
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
```

---

## 🎮 Uso

### Executar o projeto

```bash
dotnet run
```

A API estará disponível em:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger:** `https://localhost:5001/swagger`

### Executar com Hot Reload

```bash
dotnet watch run
```

---

## 🔌 Endpoints da API

### Autenticação

#### POST /api/auth/login
Realiza login e retorna token JWT

**Request:**
```json
{
  "email": "usuario@email.com",
  "senha": "senha123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "usuario": {
    "id": 1,
    "nome": "João Silva",
    "email": "usuario@email.com"
  },
  "expiration": "2024-12-13T10:30:00Z"
}
```

#### POST /api/auth/registrar
Registra um novo usuário

**Request:**
```json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "senha": "senha123",
  "confirmarSenha": "senha123"
}
```

---

### Usuários

#### GET /api/user
Lista todos os usuários (com paginação)

**Query Parameters:**
- `pageNumber` (int) - Número da página (padrão: 1)
- `pageSize` (int) - Itens por página (padrão: 10)

**Response:**
```json
{
  "items": [
    {
      "id": 1,
      "nome": "João Silva",
      "email": "joao@email.com",
      "ativo": true
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 5,
  "totalCount": 50
}
```

#### GET /api/user/{id}
Busca usuário por ID

#### PUT /api/user/{id}
Atualiza dados do usuário

#### DELETE /api/user/{id}
Desativa/remove usuário

---

## 🎨 Frontend

### Repositório do Frontend

O frontend deste projeto está disponível em um repositório separado:

**🔗 [Link do Repositório Frontend](https://github.com/gildevson/SystemPDVFrontEnd)**

### Tecnologias do Frontend
- React / Angular / Vue.js (especifique a sua)
- TypeScript
- Axios para consumo da API
- Tailwind CSS / Material-UI (especifique a sua)

### Executando Frontend e Backend juntos

1. Clone e configure o frontend conforme instruções do repositório
2. Execute o backend: `dotnet run`
3. Execute o frontend: `npm start` (ou comando específico)
4. Acesse: `http://localhost:3000` (porta padrão do frontend)

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Siga os passos:

1. Fork o projeto
2. Crie sua feature branch (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

### Padrões de Código

- Use nomenclatura clara e descritiva
- Siga os princípios SOLID
- Mantenha a separação de responsabilidades
- Adicione comentários quando necessário
- Escreva testes para novas funcionalidades

---

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👤 Autor

**Gilson**

- GitHub: (https://github.com/gildevson)
- LinkedIn: ((https://www.linkedin.com/in/gilson-fonseca-78b6b4138/)
- Email: gildevson@gmail.com


<div align="center">
  
**⭐ Se este projeto te ajudou, considere dar uma estrela! ⭐**

Feito com ❤️ por [Seu Nome](https://github.com/seu-usuario)

</div>
