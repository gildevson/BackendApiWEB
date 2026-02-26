using BackendApiWEB.Models;
using System.Data;

namespace BackendApiWEB.Data.Interfaces {
    public interface IProdutoRepository {
        IDbConnection GetConnection();
        Produtos? GetById(Guid id);
        Guid Insert(Produtos produto, IDbConnection conn, IDbTransaction? tran = null);
        bool Update(Produtos produto, IDbConnection conn, IDbTransaction? tran = null);
        bool Delete(Guid id, IDbConnection conn, IDbTransaction? tran = null);
        IEnumerable<Produtos> ListAll();
    }
}