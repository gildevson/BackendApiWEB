using BackendApiWEB.Models;
using Dapper;
namespace BackendApiWEB.Data.Interfaces
{
    public interface IPermissaoRepository
    {
        IEnumerable<Permissao> GetByUsuario(Guid usuarioId);
        bool AddDefaultPermission(Guid usuarioId);
        bool AddPermission(Guid usuarioId, int permissaoId);
        bool RemovePermission(Guid usuarioId, int permissaoId);
    }
}
