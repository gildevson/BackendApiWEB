using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Models;
using Dapper;

namespace BackendApiWEB.Data.Repositories
{
    public class PermissaoRepository : IPermissaoRepository
    {
        private readonly DbContextDapper _dapper;

        public PermissaoRepository(DbContextDapper dapper)
        {
            _dapper = dapper;
        }

        public IEnumerable<Permissao> GetByUsuario(Guid usuarioId)
        {
            string sql = @"
                SELECT P.* 
                FROM UsuarioPermissao UP
                INNER JOIN Permissoes P ON P.Id = UP.PermissaoId
                WHERE UP.UsuarioId = @usuarioId";

            using var conn = _dapper.CreateConnection();
            return conn.Query<Permissao>(sql, new { usuarioId });
        }

        public bool AddDefaultPermission(Guid usuarioId)
        {
            string sql = @"INSERT INTO UsuarioPermissao (UsuarioId, PermissaoId)
                           VALUES (@UsuarioId, 2)";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, new { UsuarioId = usuarioId }) > 0;
        }

        public bool AddPermission(Guid usuarioId, int permissaoId)
        {
            string sql = @"INSERT INTO UsuarioPermissao (UsuarioId, PermissaoId)
                           VALUES (@UsuarioId, @PermissaoId)";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, new
            {
                UsuarioId = usuarioId,
                PermissaoId = permissaoId
            }) > 0;
        }

        public bool RemovePermission(Guid usuarioId, int permissaoId)
        {
            string sql = @"DELETE FROM UsuarioPermissao 
                           WHERE UsuarioId = @UsuarioId AND PermissaoId = @PermissaoId";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, new
            {
                UsuarioId = usuarioId,
                PermissaoId = permissaoId
            }) > 0;
        }
    }
}
