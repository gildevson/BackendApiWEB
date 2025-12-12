Explicação completa de cada pasta e arquivo

📦 Controllers/
É onde ficam os endpoints da API.
Cada controller recebe requisições HTTP, chama os serviços e devolve respostas.

AuthController.cs → controla login e registro de usuário
TesteController.cs → usado para testes simples
UserController.cs → lista usuários, busca por ID, paginação etc.
WeatherForecastController.cs → arquivo padrão do template do .NET (pode excluir)

Resumo:
➡️ Controladores → "Porta de entrada" da API. Nada de regra de negócio aqui.

📦 Data/
Contém tudo relacionado ao acesso ao banco.
📄 DbContextDapper.cs
Classe que configura sua conexão com SQL Server usando Dapper.
📁 interfaces/
Interfaces dos repositórios — definem os métodos sem implementação.
Ex.: IUserRepository, IPermissaoRepository, etc.
📁 Repositories/
Implementações reais que executam SQL no banco usando Dapper.
Resumo:
➡️ Repositórios acessam o banco.
➡️ Controllers nunca falam direto com o banco — sempre via services que usam repositórios.

📦 DataBase/
Provavelmente contém arquivos utilitários, scripts ou classes auxiliares para banco de dados.
Se estiver vazio, pode ser para organização futura.

📦 DTOs/
Contém todos os objetos utilizados para entrada e saída da API (payloads).
Exemplos:

AuthResult.cs → retorno do login
LoginDTO / LoginRequest.cs → dados enviados no login
PaginatedResult.cs → modelo de paginação
RegistrarRequest.cs → dados de criação de usuário
UserResponse / UsuarioCreateDTO.cs → respostas do usuário

Resumo:
➡️ DTO = o que chega na API e o que sai da API.
➡️ Nunca expor entidades do banco diretamente.

📦 Models/
São suas entidades do domínio, que representam tabelas do banco.

Permissao.cs → entidade de permissão
Usuario.cs → entidade do usuário
UsuarioPermissao.cs → relacionamento entre usuário e permissão

Resumo:
➡️ Models refletem tabelas do banco.
➡️ DTOs refletem dados enviados/recebidos pela API.

📦 Service/
Contém a lógica de negócio da aplicação.
📁 Implementations/
Implementações concretas da regra de negócio.
Exemplos:

AuthService.cs → valida credenciais, gera JWT, chama repositório
UserService.cs → regras de CRUD de usuário
PermissaoService.cs → regras sobre permissões

📁 Interfaces/
As assinaturas das classes de serviço:

IAuthService.cs
IUserService.cs
IPermissaoService.cs

Resumo:
➡️ Services = regras de negócio.
➡️ Regras ficam aqui (não nos controllers e não no repositório).

📄 appsettings.json
Arquivo de configuração geral:

String de conexão com o banco
JWT Secret
Configurações de logs, etc.


📄 BackendApiWEB.http
Arquivo para testar requisições dentro do VS Code / Visual Studio.

📄 Program.cs
Ponto inicial da aplicação.
Configura:

Injeção de dependências
Middlewares
CORS
Swagger
Roteamento
Inicialização da API
