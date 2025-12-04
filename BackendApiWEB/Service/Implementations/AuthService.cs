using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _usuarios;
        private readonly IPermissaoRepository _permissoes;

        public AuthService(IUserRepository usuarios, IPermissaoRepository permissoes)
        {
            _usuarios = usuarios;
            _permissoes = permissoes;
        }

        // ===========================
        // LOGIN
        // ===========================
        public AuthResult Login(LoginRequest request)
        {
            var usuario = _usuarios.GetByEmail(request.Email);

            if (usuario == null)
                return new AuthResult(false, "Usuário não encontrado.", null);

            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return new AuthResult(false, "Senha incorreta.", null);

            var userResponse = new UserResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao
            };

            return new AuthResult(true, "Login realizado com sucesso!", userResponse);
        }

        // ===========================
        // REGISTRAR
        // ===========================
        public AuthResult Registrar(RegistrarRequest request)
        {
            var existente = _usuarios.GetByEmail(request.Email);

            if (existente != null)
                return new AuthResult(false, "E-mail já está cadastrado.", null);

            var novo = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                DataCriacao = DateTime.Now
            };

            var criado = _usuarios.Create(novo); // GARANTA QUE ESSE MÉTODO RETORNE BOOL

            if (!criado)
                return new AuthResult(false, "Erro ao registrar usuário.", null);

            // PERMISSÃO PADRÃO
            var permissaoCriada = _permissoes.AddDefaultPermission(novo.Id);

            if (!permissaoCriada)
                return new AuthResult(false, "Usuário criado, mas não foi possível atribuir a permissão.", null);

            return new AuthResult(true, "Usuário registrado com sucesso!", null);
        }

        // ===========================
        // DELETE
        // ===========================
        public AuthResult Delete(Guid id)
        {
            var user = _usuarios.GetById(id);

            if (user == null)
                return new AuthResult(false, "Usuário não encontrado.", null);

            var deleted = _usuarios.Delete(id);

            if (!deleted)
                return new AuthResult(false, "Erro ao deletar usuário.", null);

            return new AuthResult(true, "Usuário deletado com sucesso.", null);
        }
    }
}
