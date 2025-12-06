using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service.Implementations {
    public class AuthService : IAuthService {
        private readonly IUserRepository _usuarios;
        private readonly IPermissaoRepository _permissoes;

        public AuthService(IUserRepository usuarios, IPermissaoRepository permissoes) {
            _usuarios = usuarios;
            _permissoes = permissoes;
        }

        // ===========================
        // LOGIN
        // ===========================
        public AuthResult Login(LoginRequest request) {
            var usuario = _usuarios.GetByEmail(request.Email);

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
        public AuthResult Registrar(RegistrarRequest dto) {
            if (string.IsNullOrWhiteSpace(dto.Nome) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Senha)) {
                return new AuthResult(false, "Todos os campos são obrigatórios.", null);
            }

            // Verifica se o email já existe
            var existente = _usuarios.GetByEmail(dto.Email);
            if (existente != null) {
                return new AuthResult(false, "Este email já está cadastrado.", null);
            }

            if (dto.Senha.Length < 6) {
                return new AuthResult(false, "A senha deve ter no mínimo 6 caracteres.", null);
            }

            var novoUsuario = new Usuario {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                DataCriacao = DateTime.Now
            };

            var criado = _usuarios.Create(novoUsuario);

            if (!criado)
                return new AuthResult(false, "Erro ao criar usuário.", null);

            return new AuthResult(true, "Usuário criado com sucesso!", null);
        }

        // ===========================
        // DELETE
        // ===========================
        public AuthResult Delete(Guid id) {
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
