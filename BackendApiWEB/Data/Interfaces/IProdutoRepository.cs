using System.Data;
using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces {
    public interface IProdutoRepository {
        IDbConnection GetConnection();
        Produto? GetById(int id);
        int Insert(Produto produto, IDbConnection conn, IDbTransaction? tran = null);
        bool Update(Produto produto, IDbConnection conn, IDbTransaction? tran = null);
        bool Delete(int id, IDbConnection conn, IDbTransaction? tran = null);
        IEnumerable<Produto> ListAll(IDbConnection conn);
    }
}