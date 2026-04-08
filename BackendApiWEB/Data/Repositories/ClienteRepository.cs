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
            (@"SELECT id, Nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm FROM dbo.Clientes where id = @id", new { id});

        public IEnumerable<Cliente> ListAll()
            => _conn.Query<Cliente>
            (@"SELECT id, Nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm FROM dbo.Clientes");


        public Guid Insert(Produtos p, IDbConnection conn, IDbTransaction? tran = null)
        {
            conn.Execute(
                @"INSERT INTO dbo.Produtos (Id, Nome, Descricao, Preco, Estoque, Ativo, CriadoEm)
          VALUES (@Id, @Nome, @Descricao, @Preco, @Estoque, @Ativo, @CriadoEm);",
                new { p.Id, p.Nome, p.Descricao, p.Preco, p.Estoque, p.Ativo, p.CriadoEm },
                tran);
                return p.Id;
        }


        public Guid Insert(Cliente cliente, IDbConnection conn, IDbTransaction? tran = null)
        {
            conn.Execute(@"INSERT INTO dbo.Clientes (id, nome, CnpjCpf, Email, Telefone, Endereco, Cidade, Estado, CEP, Ativo, CriadoEm, AtualizadoEm);",
                new { cliente.Id, cliente.nome, cliente.CnpjCpf, cliente.Email }, 
                tran);
            return cliente.id; 
            
        }
    }
}
