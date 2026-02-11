using BackendApiWEB.DTOs;
using BackendApiWEB.Models;


namespace BackendApiWEB.Service.Interfaces {
    public class IProdutoService {
        Produto? GetById(int id); // Obter um produto por ID
        IEnumerable<Produto> ListAll(); // Listar todos os produtos
        int Create(ProdutoCreateDTO produtoDto); // Criar um novo produto
        bool Update(int id, ProdutoUpdateDTO produtoDto); // Atualizar um produto existente
        bool Delete(int id); // Excluir um produto por ID

    }
}
