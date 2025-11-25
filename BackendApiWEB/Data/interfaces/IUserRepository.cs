using BackendApiWEB.DTOs;
using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces
{
    public interface IUserRepository
    {
        // BÁSICO
        Usuario? GetByEmail(string email);
        Usuario? GetById(Guid id);

        // CRUD
        bool Create(Usuario usuario);
        bool Update(Guid id, UsuarioCreateDTO dto);
        bool Delete(Guid id);

        // LISTAGEM
        IEnumerable<Usuario> GetPaged(int page, int pageSize);
        int Count();
    }
}
