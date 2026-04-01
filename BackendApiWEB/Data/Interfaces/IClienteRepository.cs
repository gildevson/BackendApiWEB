using System.Data;
using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces
{
    public interface IClienteRepository
    {

        IDbConnection GetDbConnection();

        Cliente? GetById(Guid id);

        Guid Insert(Produtos produto, IDbConnection conn, IDbConnection? tran = null);

        bool Update(Produtos produto, IDbConnection conn, IDbConnection? tran = null);

        bool Delete(Guid id, IDbConnection conn, IDbConnection? tran = null);

        IEnumerable<Produtos> ListAll();



    }
}
