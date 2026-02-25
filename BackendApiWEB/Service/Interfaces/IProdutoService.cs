using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using System.Collections.Generic;

namespace BackendApiWEB.Service.Interfaces {
    public interface IProdutoService {
        Produtos? GetById(Guid id);
        IEnumerable<Produtos> ListAll();
        int Create(ProdutoCreateRequest dto);
        bool Update(Guid id, ProdutoCreateRequest dto);
        bool Delete(Guid id);
    }
}