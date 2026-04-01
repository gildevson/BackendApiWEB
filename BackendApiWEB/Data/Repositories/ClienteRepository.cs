using Dapper;
using System.Data;
using BackendApiWEB.Models;
using BackendApiWEB.Data.Repositories;
using BackendApiWEB.Data.Interfaces;


namespace BackendApiWEB.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _conn;

        public ClienteRepository(IDbConnection conn) => _conn = conn;

        public IDbConnection GetConnection() => _conn;

        public Cliente? GetClientes(Guid id) => _conn.QueryFirstOrDefault<Clientes>
    }
}
