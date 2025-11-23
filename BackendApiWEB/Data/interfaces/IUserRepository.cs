using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces {
    public interface IUserRepository {
        Usuario GetByEmail(string email);
        Usuario GetById(Guid id);
        IEnumerable<Usuario> GetAll();
        bool Create(Usuario usuario);
        IEnumerable<Permissao> GetPermissoes(Guid usuarioId);
        Usuario GetByDataCriacao(DateTime data);

        // ❌ ESTE AQUI ESTÁ FALTANDO!
        bool InserirPermissaoPadrao(Guid usuarioId);
    }
}