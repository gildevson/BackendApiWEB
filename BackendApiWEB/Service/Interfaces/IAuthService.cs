using BackendApiWEB.DTOs;

namespace BackendApiWEB.Service.Interfaces {
    public interface IAuthService {
        AuthResult Login(LoginRequest request);
        AuthResult Registrar(RegistrarRequest request);
        AuthResult Delete(Guid id);
        // 🔥 ADICIONE ESTES 3 MÉTODOS:
        AuthResult SolicitarResetSenha(ResetSenhaSolicitarRequest dto);
        AuthResult ValidarToken(string token);
        AuthResult ResetarSenha(ResetSenhaRequest dto);

        AuthResult Update(UpdateUserRequest request);

    }
}
