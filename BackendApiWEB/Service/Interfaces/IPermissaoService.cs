using BackendApiWEB.Models;

namespace BackendApiWEB.Service.Interfaces
{
    public interface IPermissaoService
    {
        IEnumerable<Permissao> GetByUsuario(Guid usuarioId);
        bool AddDefault(Guid usuarioId);
        bool Add(Guid usuarioId, int permissaoId);
        bool Remove(Guid usuarioId, int permissaoId);
    }
}
