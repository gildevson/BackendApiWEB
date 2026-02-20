using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using System.Collections.Generic;

namespace BackendApiWEB.Service.Interfaces {
    public interface IProdutoService {
        Produtos? GetById(int id);
        IEnumerable<Produtos> ListAll();
        int Create(ProdutoCreateRequest dto);
        bool Update(int id, ProdutoCreateRequest dto);
        bool Delete(int id);
    }
}