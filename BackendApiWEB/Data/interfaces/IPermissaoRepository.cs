using BackendApiWEB.Models;
using System.Data;

using BackendApiWEB.Models;
using System.Data;

namespace BackendApiWEB.Data.Interfaces {
    public interface IPermissaoRepository {
        IEnumerable<Permissao> GetByUsuario(Guid usuarioId);

        bool AddPermission(Guid usuarioId, int permissaoId, IDbConnection conn, IDbTransaction tran);
        bool AddDefaultPermission(Guid usuarioId, IDbConnection conn, IDbTransaction tran);
        bool RemovePermission(Guid usuarioId, int permissaoId, IDbConnection conn, IDbTransaction tran);

        // 🆕 Novos métodos
        bool RemoveAllPermissions(Guid usuarioId, IDbConnection conn, IDbTransaction tran);
        IEnumerable<int> GetUserPermissions(Guid usuarioId);
    }
}