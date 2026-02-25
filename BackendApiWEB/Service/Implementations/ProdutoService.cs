using System;
using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service.Implementations {
    public class ProdutoService : IProdutoService {
        private readonly IProdutoRepository _repo;

        public ProdutoService(IProdutoRepository repo) {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public Produtos? GetById(int id) {
            return _repo.GetById(id);
        }

        public IEnumerable<Produtos> ListAll() {
            return _repo.ListAll();
        }

        public int Create(ProdutoCreateRequest dto) {
            var p = new Produtos {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                Estoque = dto.Estoque,
                Ativo = dto.Ativo,
                CriadoEm = DateTime.UtcNow
            };

            using var conn = _repo.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try {
                var id = _repo.Insert(p, conn, tran);
                tran.Commit();
                return id;
            } catch {
                tran.Rollback();
                throw;
            }
        }

        public bool Update(int id, ProdutoCreateRequest dto) {
            var existing = _repo.GetById(id);
            if (existing == null) return false;

            existing.Nome = dto.Nome;
            existing.Descricao = dto.Descricao;
            existing.Preco = dto.Preco;
            existing.Estoque = dto.Estoque;
            existing.Ativo = dto.Ativo;
            existing.AtualizadoEm = DateTime.UtcNow;

            using var conn = _repo.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try {
                var ok = _repo.Update(existing, conn, tran);
                tran.Commit();
                return ok;
            } catch {
                tran.Rollback();
                throw;
            }
        }

        public bool Delete(int id) {
            using var conn = _repo.GetConnection();
            conn.Open();
            using var tran = conn.BeginTransaction();
            try {
                var ok = _repo.Delete(id, conn, tran);
                tran.Commit();
                return ok;
            } catch {
                tran.Rollback();
                throw;
            }
        }
    }
}