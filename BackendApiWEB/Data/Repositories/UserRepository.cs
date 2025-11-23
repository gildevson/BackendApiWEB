using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Models;
using Dapper;

namespace BackendApiWEB.Data.Repositories {
    public class UserRepository : IUserRepository {
        private readonly DbContextDapper _dapper;

        public UserRepository(DbContextDapper dapper) {
            _dapper = dapper;
        }

       

        public Usuario GetByEmail(string email) {
            string sql = "SELECT * FROM Usuarios WHERE Email = @email";
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(sql, new { email });
        }

        public Usuario GetById(Guid id) {
            string sql = "SELECT * FROM Usuarios WHERE Id = @id";
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(sql, new { id });
        }

        public IEnumerable<Usuario> GetAll() {
            string sql = @"
            SELECT 
            Id,
            Nome,
            Email,
            SenhaHash,
            DataCriacao
            FROM Usuarios
            ORDER BY DataCriacao DESC
            ";

            using var conn = _dapper.CreateConnection();
            return conn.Query<Usuario>(sql);
        }

        public bool Create(Usuario usuario) {
            string sql = @"
                INSERT INTO Usuarios (Id, Nome, Email, SenhaHash, DataCriacao)
                VALUES (@Id, @Nome, @Email, @SenhaHash, @DataCriacao)
            ";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, usuario) > 0;
        }

        public IEnumerable<Permissao> GetPermissoes(Guid usuarioId) {
            string sql = @"
                SELECT P.* 
                FROM UsuarioPermissao UP
                INNER JOIN Permissoes P ON P.Id = UP.PermissaoId
                WHERE UP.UsuarioId = @usuarioId
            ";

            using var conn = _dapper.CreateConnection();
            return conn.Query<Permissao>(sql, new { usuarioId });
        }

        public Usuario GetByDataCriacao(DateTime data) {
            string sql = "SELECT * FROM Usuarios WHERE CAST(DataCriacao AS DATE) = CAST(@data AS DATE)";
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(sql, new { data });
        }
        public bool InserirPermissaoPadrao(Guid usuarioId) {
            string sql = @"
        INSERT INTO UsuarioPermissao (UsuarioId, PermissaoId)
        VALUES (@UsuarioId, 2); -- 2 = USUARIO
    ";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, new { UsuarioId = usuarioId }) > 0;
        }


    }
}
