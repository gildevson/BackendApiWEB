using Microsoft.Data.SqlClient;
using System.Data;

namespace BackendApiWEB.Data {
    public class DbContextDapper {
        private readonly string _connectionString;

        public DbContextDapper(IConfiguration configuration) {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
