using BackendApiWEB.Data.Interfaces;
using Dapper;
using System;

namespace BackendApiWEB.Data.Repositories
{
    public class ResetSenhaRepository : IResetSenhaRepository
    {
        private readonly DbContextDapper _db;

        public ResetSenhaRepository(DbContextDapper db)
        {
            _db = db;
        }

        public void CriarToken(Guid usuarioId, Guid token, DateTime expiracao)
        {
            string sql = @"
                INSERT INTO ResetSenhaTokens (UsuarioId, Token, Expiracao, Usado)
                VALUES (@UsuarioId, @Token, @Expiracao, 0)
            ";

            _db.Execute(sql, new { UsuarioId = usuarioId, Token = token, Expiracao = expiracao });
        }

        public bool TokenValido(string token)
        {
            string sql = @"
                SELECT COUNT(*) 
                FROM ResetSenhaTokens
                WHERE Token = @Token
                AND Usado = 0
                AND Expiracao >= GETDATE()
            ";

            return _db.ExecuteScalar<int>(sql, new { Token = token }) > 0;
        }

        public Guid? ObterUsuarioPorToken(string token)
        {
            string sql = @"
                SELECT UsuarioId
                FROM ResetSenhaTokens
                WHERE Token = @Token
                AND Usado = 0
            ";

            return _db.ExecuteScalar<Guid?>(sql, new { Token = token });
        }

        public void MarcarTokenComoUsado(string token)
        {
            string sql = @"
                UPDATE ResetSenhaTokens
                SET Usado = 1
                WHERE Token = @Token
            ";

            _db.Execute(sql, new { Token = token });
        }
    }
}
