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

        public Produtos? GetById(int id) // esse aqui é basicamente o método para pegar um produto pelo id, ele retorna um objeto do tipo Produtos ou null se não encontrar, ele usa o Dapper para executar a query SQL e mapear o resultado para o objeto Produtos
            => _conn.QueryFirstOrDefault<Produtos>
            (@"SELECT id, Nome, Descricao, Preco, Estoque, Ativo, CriadoEm, AtualizadoEm FROM dbo.Produtos Where id = @id", new { id });

        public IEnumerable<Produtos> ListAll()
            => _conn.Query<Produtos>
            (@"SELECT id, Nome, Descricao, Preco, Estoque, Ativo, CriadoEm, AtualizadoEm FROM dbo.Produtos");

        public int insert(Produtos p, IDbConnection conn, IDbTransaction? tran = null)
            => conn.ExecuteScalar<int>(
                @"INSERT INTO dbo.Produtos (Nome, Descricao, Preco, Estoque, Ativo, CriadoEm) VALUES (@Nome, @Descricao, @Preco, @Estoque, @Ativo, @CriadoEm); SELECT CAST(SCOPE_IDENTITY() as int)",
                new { p.Nome, p.Descricao, p.Preco, p.Estoque.Length, p.Ativo }, tran);

        public bool Update(Produtos p, IDbConnection conn, IDbTransaction? tran = null)
            => conn.Execute(
                @"UPDATE dbo.Produtos SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, Estoque = @Estoque, Ativo = @Ativo, AtualizadoEm = @AtualizadoEm WHERE id = @id",
                new { p.Nome, p.Descricao, p.Preco, p.Estoque.Length, p.Ativo, p.AtualizadoEm, p.id }, tran) > 0;

        public bool Delete(int id, IDbConnection conn, IDbTransaction ? tran = null)
            => conn.Execute(@"DELETE FROM dbo.Produtos WHERE id=@id", new { id}, tran) > 0;

        IDbConnection IProdutoRepository.GetConnection() {
            throw new NotImplementedException();
        }

        Produtos? IProdutoRepository.GetById(int id) {
            throw new NotImplementedException();
        }

        int IProdutoRepository.Insert(Produtos produto, IDbConnection conn, IDbTransaction? tran) {
            throw new NotImplementedException();
        }

        bool IProdutoRepository.Update(Produtos produto, IDbConnection conn, IDbTransaction? tran) {
            throw new NotImplementedException();
        }

        bool IProdutoRepository.Delete(int id, IDbConnection conn, IDbTransaction? tran) {
            throw new NotImplementedException();
        }

        IEnumerable<Produtos> IProdutoRepository.ListAll() {
            throw new NotImplementedException();
        }
    }
}
