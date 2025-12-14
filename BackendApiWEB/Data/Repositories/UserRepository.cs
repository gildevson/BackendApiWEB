using BackendApiWEB.Data;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using Dapper;

namespace BackendApiWEB.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextDapper _dapper;

        public UserRepository(DbContextDapper dapper)
        {
            _dapper = dapper;
        }

        public Usuario? GetByEmail(string email)
        {
            const string sql = "SELECT * FROM Usuarios WHERE Email = @email";
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(sql, new { email });
        }

        public Usuario? GetById(Guid id)
        {
            const string sql = "SELECT * FROM Usuarios WHERE Id = @id";
            using var conn = _dapper.CreateConnection();
            return conn.QueryFirstOrDefault<Usuario>(sql, new { id });
        }

        public bool Create(Usuario usuario)
        {
            const string sql = @"
                INSERT INTO Usuarios (Id, Nome, Email, SenhaHash, DataCriacao)
                VALUES (@Id, @Nome, @Email, @SenhaHash, @DataCriacao)";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, usuario) > 0;
        }

        public bool Update(Guid id, UsuarioCreateDTO dto)
        {
            const string sql = @"
                UPDATE Usuarios
                SET Nome = @Nome, Email = @Email
                WHERE Id = @Id";

            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, new { Id = id, dto.Nome, dto.Email }) > 0;
        }

        public bool Delete(Guid id)
        {
            const string sql = "DELETE FROM Usuarios WHERE Id = @Id";
            using var conn = _dapper.CreateConnection();
            return conn.Execute(sql, new { Id = id }) > 0;
        }

        public IEnumerable<Usuario> GetPaged(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            const string sql = @"
                SELECT *
                FROM Usuarios
                ORDER BY DataCriacao DESC
                OFFSET @skip ROWS FETCH NEXT @pageSize ROWS ONLY";

            using var conn = _dapper.CreateConnection();
            return conn.Query<Usuario>(sql, new { skip, pageSize });
        }

        public int Count()
        {
            const string sql = "SELECT COUNT(*) FROM Usuarios";
            using var conn = _dapper.CreateConnection();
            return conn.ExecuteScalar<int>(sql);
        }

        // 🔥 IMPLEMENTAÇÃO FINAL DO RESET DE SENHA
        public bool AlterarSenha(Guid id, string novaSenhaHash)
        {
            const string sql = @"
                UPDATE Usuarios
                SET SenhaHash = @SenhaHash
                WHERE Id = @Id;
            ";

            using var conn = _dapper.CreateConnection();

            int linhas = conn.Execute(sql, new { Id = id, SenhaHash = novaSenhaHash });

            return linhas > 0;
        }

        public bool Update(Usuario usuario) {
            const string sql = @"
        UPDATE Usuarios
        SET Nome = @Nome,
            Email = @Email,
            SenhaHash = @SenhaHash
        WHERE Id = @Id
    ";

            using var conn = _dapper.CreateConnection();
            var rows = conn.Execute(sql, usuario);

            return rows > 0;
        }
    }
}
