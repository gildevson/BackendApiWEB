using BackendApiWEB.DTOs;

namespace BackendApiWEB.Service.Interfaces {
    public interface IAuthService {
        AuthResult Login(LoginRequest request);
        AuthResult Registrar(RegistrarRequest request);
        AuthResult Delete(Guid id);
    }
}
