using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;
using BCrypt.Net;

namespace BackendApiWEB.Service {
    public class AuthService : IAuthService {
        private readonly IUserRepository _repo;

        public AuthService(IUserRepository repo) {
            _repo = repo;
        }

        // ===========================
        // LOGIN
        // ===========================
        public AuthResult Login(LoginRequest request) {
            var usuario = _repo.GetByEmail(request.Email);

            if (usuario == null)
                return new AuthResult(false, "Usuário não encontrado.", null);

            if (!BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return new AuthResult(false, "Senha incorreta.", null);

            var userResponse = new UserResponse {
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
        public AuthResult Registrar(RegistrarRequest request) {
            var existente = _repo.GetByEmail(request.Email);

            if (existente != null)
                return new AuthResult(false, "E-mail já está cadastrado.", null);

            var novo = new Usuario {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                DataCriacao = DateTime.Now
            };

            var criado = _repo.Create(novo);

            if (!criado)
                return new AuthResult(false, "Erro ao registrar usuário.", null);

            var permissaoCriada = _repo.InserirPermissaoPadrao(novo.Id);

            if (!permissaoCriada)
                return new AuthResult(false, "Usuário criado mas não foi possível atribuir a permissão.", null);

            return new AuthResult(true, "Usuário registrado com sucesso!", null);
        }
    }
}
