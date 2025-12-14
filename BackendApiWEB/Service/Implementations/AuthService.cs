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
        private readonly IResetSenhaRepository _resetSenha;
        private readonly IEmailService _email;

        public AuthService(
            IUserRepository usuarios,
            IPermissaoRepository permissoes,
            IResetSenhaRepository resetSenha,
            IEmailService email)
        {
            _usuarios = usuarios;
            _permissoes = permissoes;
            _resetSenha = resetSenha;
            _email = email;
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
        public AuthResult Registrar(RegistrarRequest dto) {
            // ===========================
            // VALIDAÇÕES
            // ===========================
            if (string.IsNullOrWhiteSpace(dto.Nome) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Senha))
                return new AuthResult(false, "Todos os campos são obrigatórios.", null);

            if (dto.Senha.Length < 6)
                return new AuthResult(false, "A senha deve ter no mínimo 6 caracteres.", null);

            var existente = _usuarios.GetByEmail(dto.Email);
            if (existente != null)
                return new AuthResult(false, "Este email já está cadastrado.", null);

            var novoUsuario = new Usuario {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                DataCriacao = DateTime.Now
            };

            // ===========================
            // 🔥 TRANSAÇÃO
            // ===========================
            using var conn = _usuarios.GetConnection(); // 👈 explico abaixo
            conn.Open();

            using var tran = conn.BeginTransaction();

            try {
                var criado = _usuarios.Create(novoUsuario, conn, tran);
                if (!criado)
                    throw new Exception("Erro ao criar usuário.");

                bool permissaoCriada;

                if (dto.PermissaoId.HasValue) {
                    permissaoCriada = _permissoes.AddPermission(
                        novoUsuario.Id,
                        dto.PermissaoId.Value,
                        conn,
                        tran
                    );
                } else {
                    permissaoCriada = _permissoes.AddDefaultPermission(
                        novoUsuario.Id,
                        conn,
                        tran
                    );
                }

                if (!permissaoCriada)
                    throw new Exception("Erro ao vincular permissão.");

                tran.Commit();
                return new AuthResult(true, "Usuário criado com sucesso!", null);
            } catch {
                tran.Rollback();
                throw;
            }
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

        // ===========================
        // SOLICITAR RESET SENHA.
        // ===========================
        public AuthResult SolicitarResetSenha(ResetSenhaSolicitarRequest dto)
        {
            var usuario = _usuarios.GetByEmail(dto.Email);

            if (usuario == null)
                return new AuthResult(false, "E-mail não encontrado.", null);

            var token = Guid.NewGuid();
            var expiracao = DateTime.Now.AddHours(1);

            _resetSenha.CriarToken(usuario.Id, token, expiracao);

            var link = $"http://localhost:4200/redefinirsenha?token={token}";

            _email.Enviar(
                usuario.Email,
                "Redefinição de Senha",
                $"Clique no link para redefinir sua senha:<br><br><a href='{link}'>Redefinir Senha</a>"
            );

            return new AuthResult(true, "E-mail enviado com sucesso!", null);
        }

        // ===========================
        // VALIDAR TOKEN
        // ===========================
        public AuthResult ValidarToken(string token)
        {
            var valido = _resetSenha.TokenValido(token);

            if (!valido)
                return new AuthResult(false, "Token inválido ou expirado.", null);

            return new AuthResult(true, "Token válido.", null);
        }

        // ===========================
        // RESETAR SENHA
        // ===========================
        public AuthResult ResetarSenha(ResetSenhaRequest dto)
        {
            var usuarioId = _resetSenha.ObterUsuarioPorToken(dto.Token);

            if (usuarioId == null)
                return new AuthResult(false, "Token inválido.", null);

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.NovaSenha);

            _usuarios.AlterarSenha(usuarioId.Value, hash);
            _resetSenha.MarcarTokenComoUsado(dto.Token);

            return new AuthResult(true, "Senha alterada com sucesso!", null);
        }

        public AuthResult Update(UpdateUserRequest dto) {
            var usuario = _usuarios.GetById(dto.Id);

            if (usuario == null)
                return new AuthResult(false, "Usuário não encontrado.", null);

            // Verifica se o email já existe em outro usuário
            var emailExistente = _usuarios.GetByEmail(dto.Email);
            if (emailExistente != null && emailExistente.Id != dto.Id)
                return new AuthResult(false, "Este e-mail já está em uso.", null);

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;

            // Atualiza senha apenas se vier preenchida
            if (!string.IsNullOrWhiteSpace(dto.Senha)) {
                if (dto.Senha.Length < 6)
                    return new AuthResult(false, "A senha deve ter no mínimo 6 caracteres.", null);

                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            }

            var atualizado = _usuarios.Update(usuario);

            if (!atualizado)
                return new AuthResult(false, "Erro ao atualizar usuário.", null);

            return new AuthResult(true, "Usuário atualizado com sucesso.", null);
        }

    }

    
}
