using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace BackendApiWEB.Data
{
    public class DbContextDapper
    {
        private readonly string _connectionString;

        public DbContextDapper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        // ============================
        // 🔥 Funções Dapper adicionadas
        // ============================

        public int Execute(string sql, object param = null)
        {
            using var connection = CreateConnection();
            return connection.Execute(sql, param);
        }

        public T ExecuteScalar<T>(string sql, object param = null)
        {
            using var connection = CreateConnection();
            return connection.ExecuteScalar<T>(sql, param);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using var connection = CreateConnection();
            return connection.Query<T>(sql, param);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            using var connection = CreateConnection();
            return connection.QueryFirstOrDefault<T>(sql, param);
        }
    }
}
