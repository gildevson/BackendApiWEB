using Dapper;
using System.Data;
using BackendApiWEB.Models;
using BackendApiWEB.Data.Repositories;
using BackendApiWEB.Data.Interfaces;


namespace BackendApiWEB.Data.Repositories
{
    public class ClienteRepository : IClienteRepository {
        private readonly IDbConnection _conn;

        public ClienteRepository(IDbConnection conn) => _conn = conn;

        public IDbConnection GetConnection() => _conn;

        public Cliente? GetById(Guid id) 
            => _conn.QueryFirstOrDefault<Cliente>
            (@"SELECT id, Nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm FROM dbo.Clientes where id = @id", new { id});

        public IEnumerable<Produtos> ListAll()
            => _conn.Query<Produtos>
            (@"SELECT id, Nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm FROM dbo.Clientes");
            
    }
}
