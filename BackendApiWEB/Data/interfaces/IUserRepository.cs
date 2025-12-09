using BackendApiWEB.DTOs;
using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces
{
    public interface IUserRepository
    {
        Usuario? GetById(Guid id);
        Usuario? GetByEmail(string email);
        bool Create(Usuario usuario);
        bool Delete(Guid id);

        // 🔥 MÉTODOS OBRIGATÓRIOS PARA O UserService
        bool Update(Guid id, UsuarioCreateDTO dto);
        IEnumerable<Usuario> GetPaged(int page, int pageSize);
        int Count();

        // 🔥 Para reset de senha
        bool AlterarSenha(Guid id, string novaSenhaHash);
    }
}
