using BackendApiWEB.DTOs;

namespace BackendApiWEB.Service.Interfaces {
    public interface IAuthService {
        AuthResult Login(LoginRequest request);
        AuthResult Registrar(RegistrarRequest request);

        // ✅ fica só este
        AuthResult Delete(Guid id, Guid idLogado);

        AuthResult SolicitarResetSenha(ResetSenhaSolicitarRequest dto);
        AuthResult ValidarToken(string token);
        AuthResult ResetarSenha(ResetSenhaRequest dto);

        AuthResult Update(UpdateUserRequest request);
    }
}
