using BackendApiWEB.Models;
using BackendApiWEB.DTOs;
using System.Data;

namespace BackendApiWEB.Data.Interfaces {
    public interface IUserRepository {
        // ===========================
        // CONSULTAS
        // ===========================
        Usuario? GetById(Guid id);
        Usuario? GetByEmail(string email);
        IEnumerable<UserResponse> GetPagedWithPermissao(int page, int pageSize);
        IEnumerable<Usuario> GetPaged(int page, int pageSize);
        int Count();

        // ===========================
        // OPERAÇÕES SIMPLES (LEGADAS)
        // ===========================
        bool Create(Usuario usuario);
        bool Update(Usuario usuario);
        bool Delete(Guid id);
        bool AlterarSenha(Guid id, string novaSenhaHash);

        // ===========================
        // 🔥 OPERAÇÕES COM TRANSAÇÃO
        // ===========================
        bool Create(Usuario usuario, IDbConnection conn, IDbTransaction tran);
        bool Update(Usuario usuario, IDbConnection conn, IDbTransaction tran);
        bool Delete(Guid id, IDbConnection conn, IDbTransaction tran);
        bool AlterarSenha(Guid id, string novaSenhaHash, IDbConnection conn, IDbTransaction tran);

        // ===========================
        // 🔗 CONEXÃO
        // ===========================
        IDbConnection GetConnection();
    }
}