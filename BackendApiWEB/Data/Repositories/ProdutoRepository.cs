using Dapper;
using System.Data;
using BackendApiWEB.Models;
using BackendApiWEB.Data.Repositories;
using BackendApiWEB.Data.Interfaces;


namespace BackendApiWEB.Data.Repositories {
    public class ProdutoRepository : IProdutoRepository {

        private readonly IDbConnection _conn;
        public ProdutoRepository(IDbConnection conn) => _conn = conn; // NÃO SEI QUE ISSO SIGIFCA MAS É O CONSTRUTOR DA CLASSE, ELE RECEBE UM IDbConnection E ATRIBUI A VARIÁVEL _conn

        public IDbConnection GetConnection() => _conn;

        public Produtos? GetById(Guid id) // esse aqui é basicamente o método para pegar um produto pelo guid id, ele retorna um objeto do tipo Produtos ou null se não encontrar, ele usa o Dapper para executar a query SQL e mapear o resultado para o objeto Produtos
            => _conn.QueryFirstOrDefault<Produtos>
            (@"SELECT id, Nome, Descricao, Preco, Estoque, Ativo, CriadoEm, AtualizadoEm FROM dbo.Produtos Where id = @id", new { id });

        public IEnumerable<Produtos> ListAll()
            => _conn.Query<Produtos>
            (@"SELECT id, Nome, Descricao, Preco, Estoque, Ativo, CriadoEm, AtualizadoEm FROM dbo.Produtos");

        public Guid Insert(Produtos p, IDbConnection conn, IDbTransaction? tran = null) {
            conn.Execute(
                @"INSERT INTO dbo.Produtos (Id, Nome, Descricao, Preco, Estoque, Ativo, CriadoEm)
          VALUES (@Id, @Nome, @Descricao, @Preco, @Estoque, @Ativo, @CriadoEm);",
                new { p.Id, p.Nome, p.Descricao, p.Preco, p.Estoque, p.Ativo, p.CriadoEm },
                tran);

            return p.Id;
        }

        public bool Update(Produtos p, IDbConnection conn, IDbTransaction? tran = null)
            => conn.Execute(
                @"UPDATE dbo.Produtos 
                SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, Estoque = @Estoque, Ativo = @Ativo, AtualizadoEm = @AtualizadoEm WHERE id = @id",
                new { p.Id,p.Nome, p.Descricao, p.Preco, p.Estoque, p.Ativo, p.AtualizadoEm},
                tran) > 0;

        public bool Delete(Guid id, IDbConnection conn, IDbTransaction ? tran = null)
            => conn.Execute(@"DELETE FROM dbo.Produtos WHERE id=@id", new { id}, tran) > 0;
    }
}
