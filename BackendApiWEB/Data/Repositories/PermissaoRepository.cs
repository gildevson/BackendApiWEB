using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Models;
using Dapper;
using System.Data;

namespace BackendApiWEB.Data.Repositories {
    public class PermissaoRepository : IPermissaoRepository {
        private readonly DbContextDapper _dapper;

        public PermissaoRepository(DbContextDapper dapper) {
            _dapper = dapper;
        }

        public IEnumerable<Permissao> GetByUsuario(Guid usuarioId) {
            string sql = @"
                SELECT P.* 
                FROM UsuarioPermissao UP
                INNER JOIN Permissoes P ON P.Id = UP.PermissaoId
                WHERE UP.UsuarioId = @usuarioId";
            
            using var conn = _dapper.CreateConnection();
            return conn.Query<Permissao>(sql, new { usuarioId });
        }

        public bool AddPermission(
            Guid usuarioId,
            int permissaoId,
            IDbConnection conn,
            IDbTransaction tran) {
            
            string sql = @"
                INSERT INTO UsuarioPermissao (UsuarioId, PermissaoId)
                VALUES (@UsuarioId, @PermissaoId)";
            
            return conn.Execute(sql, new {
                UsuarioId = usuarioId,
                PermissaoId = permissaoId
            }, tran) > 0;
        }

        public bool AddDefaultPermission(
            Guid usuarioId,
            IDbConnection conn,
            IDbTransaction tran) {
            
            string sql = @"
                INSERT INTO UsuarioPermissao (UsuarioId, PermissaoId)
                VALUES (@UsuarioId, 2)";
            
            return conn.Execute(sql, new { UsuarioId = usuarioId }, tran) > 0;
        }

        public bool RemovePermission(
            Guid usuarioId,
            int permissaoId,
            IDbConnection conn,
            IDbTransaction tran) {
            
            string sql = @"
                DELETE FROM UsuarioPermissao
                WHERE UsuarioId = @UsuarioId
                  AND PermissaoId = @PermissaoId";
            
            return conn.Execute(sql, new {
                UsuarioId = usuarioId,
                PermissaoId = permissaoId
            }, tran) > 0;
        }

        // 🆕 REMOVER TODAS AS PERMISSÕES DE UM USUÁRIO
        public bool RemoveAllPermissions(
            Guid usuarioId,
            IDbConnection conn,
            IDbTransaction tran) {
            
            string sql = @"
                DELETE FROM UsuarioPermissao
                WHERE UsuarioId = @UsuarioId";
            
            conn.Execute(sql, new { UsuarioId = usuarioId }, tran);
            return true;
        }

        // 🆕 OBTER IDS DAS PERMISSÕES DE UM USUÁRIO
        public IEnumerable<int> GetUserPermissions(Guid usuarioId) {
            string sql = @"
                SELECT PermissaoId 
                FROM UsuarioPermissao 
                WHERE UsuarioId = @UsuarioId";
            
            using var conn = _dapper.CreateConnection();
            return conn.Query<int>(sql, new { UsuarioId = usuarioId });
        }
    }
}