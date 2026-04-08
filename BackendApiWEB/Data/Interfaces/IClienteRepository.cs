using System.Data;
using BackendApiWEB.Models;

namespace BackendApiWEB.Data.Interfaces {
    public interface IClienteRepository  {
        IDbConnection GetDbConnection();
        Cliente? GetById(Guid id);
        Guid Insert(Cliente Cliente, IDbConnection conn, IDbConnection? tran = null);
        bool Update(Cliente produto, IDbConnection conn, IDbConnection? tran = null);
        bool Delete(Guid id, IDbConnection conn, IDbConnection? tran = null);
        IEnumerable<Cliente> ListAll();



    }
}
