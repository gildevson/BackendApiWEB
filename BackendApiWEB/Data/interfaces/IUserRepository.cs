using BackendApiWEB.DTOs;
using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces {
    public interface IUserRepository {
        Usuario? GetById(Guid id);
        Usuario? GetByEmail(string email);

        bool Create(Usuario usuario);
        bool Update(Usuario usuario);   // ✅ ÚNICO UPDATE
        bool Delete(Guid id);

        IEnumerable<Usuario> GetPaged(int page, int pageSize);
        int Count();

        bool AlterarSenha(Guid id, string novaSenhaHash);
    }
}