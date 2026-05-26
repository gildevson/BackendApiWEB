using Dapper;
using System.Data;
using BackendApiWEB.Models;
using BackendApiWEB.Data.Interfaces;


namespace BackendApiWEB.Data.Repositories
{
    public class ClienteRepository : IClienteRepository {
        private readonly IDbConnection _conn;

        public ClienteRepository(IDbConnection conn) => _conn = conn;

        public IDbConnection GetConnection() => _conn;

        public Cliente? GetById(Guid id) 
            => _conn.QueryFirstOrDefault<Cliente>
            (@"SELECT id, Nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm FROM dbo.Clientes where id = @id", 
                new { id});

        public IEnumerable<Cliente> ListAll()
            => _conn.Query<Cliente>
            (@"SELECT id, Nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm FROM dbo.Clientes");

        
        public Guid Insert(Cliente c, IDbConnection conn, IDbTransaction? tran = null) {
            conn.Execute(@"INSERT INTO dbo.Clientes (id, nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm)
            VALUES(@id, @Nome, @Cnpj, @Email, @Telefone, @Endereco, @Cidade, @Estado, @Cep, @Ativo, @CriadoEm, @Atualizado);",
                new { c.Id, c.Nome, c.CnpjCpf, c.Email, c.Telefone, c.Endereco, c.Cidade, c.Estado, c.Cep, c.Ativo, c.CriadoEm, c.AtualizadoEm}, 
                tran);
            return c.Id; 
            
        }
    }
}
