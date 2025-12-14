using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using Dapper;
using System.Data;

namespace BackendApiWEB.Data.Repositories {
    public class UsuarioRepository : IUserRepository {
        private readonly DbContextDapper _dapper;

        public UsuarioRepository(DbContextDapper dapper) {
            _dapper = dapper;
        }

        // ===========================
        // 🔗 CONEXÃO
        // ===========================
        public IDbConnection GetConnection() {
            return _dapper.CreateConnection();
        }

        // ===========================
        // CONSULTAS
        // ===========================
        public Usuario? GetById(Guid id) {
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(
                "SELECT * FROM Usuarios WHERE Id = @Id",
                new { Id = id }
            );
        }

        public Usuario? GetByEmail(string email) {
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(
                "SELECT * FROM Usuarios WHERE Email = @Email",
                new { Email = email }
            );
        }

        public IEnumerable<Usuario> GetPaged(int page, int pageSize) {
            using var conn = _dapper.CreateConnection();

            var sql = @"
                SELECT 
                    Id,
                    Nome,
                    Email,
                    SenhaHash,
                    DataCriacao
                FROM Usuarios
                ORDER BY DataCriacao DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
            ";

            return conn.Query<Usuario>(sql, new {
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            });
        }

        public IEnumerable<UserResponse> GetPagedWithPermissao(int page, int pageSize) {
            using var conn = _dapper.CreateConnection();

            var sql = @"
                WITH UsuariosPaginados AS (
                    SELECT 
                        Id,
                        Nome,
                        Email,
                        DataCriacao,
                        ROW_NUMBER() OVER (ORDER BY DataCriacao DESC) AS RowNum
                    FROM Usuarios
                )
                SELECT 
                    U.Id,
                    U.Nome,
                    U.Email,
                    U.DataCriacao,
                    P.Nome AS PermissaoNome
                FROM UsuariosPaginados U
                LEFT JOIN UsuarioPermissao UP ON UP.UsuarioId = U.Id
                LEFT JOIN Permissoes P ON P.Id = UP.PermissaoId
                WHERE U.RowNum > @Offset AND U.RowNum <= @Offset + @PageSize
                ORDER BY U.DataCriacao DESC
            ";

            var userDict = new Dictionary<Guid, UserResponse>();

            conn.Query<UserResponse, string?, UserResponse>(
                sql,
                (user, permissao) => {
                    if (!userDict.TryGetValue(user.Id, out var userEntry)) {
                        userEntry = user;
                        userEntry.Permissoes = new List<string>();
                        userDict.Add(user.Id, userEntry);
                    }

                    if (!string.IsNullOrEmpty(permissao)) {
                        ((List<string>)userEntry.Permissoes).Add(permissao);
                    }

                    return userEntry;
                },
                new {
                    Offset = (page - 1) * pageSize,
                    PageSize = pageSize
                },
                splitOn: "PermissaoNome"
            );

            return userDict.Values;
        }

        public int Count() {
            using var conn = _dapper.CreateConnection();
            return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Usuarios");
        }

        // ===========================
        // OPERAÇÕES SIMPLES (LEGADAS)
        // ===========================
        public bool Create(Usuario usuario) {
            using var conn = _dapper.CreateConnection();
            using var tran = conn.BeginTransaction();

            try {
                var result = Create(usuario, conn, tran);
                tran.Commit();
                return result;
            } catch {
                tran.Rollback();
                throw;
            }
        }

        public bool Update(Usuario usuario) {
            using var conn = _dapper.CreateConnection();

            string sql = @"
                UPDATE Usuarios
                SET Nome = @Nome,
                    Email = @Email
                WHERE Id = @Id
            ";

            return conn.Execute(sql, usuario) > 0;
        }

        public bool Delete(Guid id) {
            using var conn = _dapper.CreateConnection();
            return conn.Execute(
                "DELETE FROM Usuarios WHERE Id = @Id",
                new { Id = id }
            ) > 0;
        }

        public bool AlterarSenha(Guid id, string novaSenhaHash) {
            using var conn = _dapper.CreateConnection();
            return conn.Execute(
                "UPDATE Usuarios SET SenhaHash = @SenhaHash WHERE Id = @Id",
                new { Id = id, SenhaHash = novaSenhaHash }
            ) > 0;
        }

        // ===========================
        // 🔥 OPERAÇÕES COM TRANSAÇÃO
        // ===========================
        public bool Create(Usuario usuario, IDbConnection conn, IDbTransaction tran) {
            string sql = @"
                INSERT INTO Usuarios (Id, Nome, Email, SenhaHash, DataCriacao)
                VALUES (@Id, @Nome, @Email, @SenhaHash, @DataCriacao)
            ";

            return conn.Execute(sql, usuario, tran) > 0;
        }

        public bool Update(Usuario usuario, IDbConnection conn, IDbTransaction tran) {
            string sql = @"
                UPDATE Usuarios
                SET Nome = @Nome,
                    Email = @Email
                WHERE Id = @Id
            ";

            return conn.Execute(sql, usuario, tran) > 0;
        }

        public bool Delete(Guid id, IDbConnection conn, IDbTransaction tran) {
            return conn.Execute(
                "DELETE FROM Usuarios WHERE Id = @Id",
                new { Id = id },
                tran
            ) > 0;
        }

        public bool AlterarSenha(Guid id, string novaSenhaHash, IDbConnection conn, IDbTransaction tran) {
            return conn.Execute(
                "UPDATE Usuarios SET SenhaHash = @SenhaHash WHERE Id = @Id",
                new { Id = id, SenhaHash = novaSenhaHash },
                tran
            ) > 0;
        }
    }
}