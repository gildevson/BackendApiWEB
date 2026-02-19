using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers {
    [ApiController]
    [Router("api/[controller]")]
    public class ProdutosController : ControllerBase {
        private readonly IProdutoService _produtoService;
        public ProdutosController(IProdutoService produtoService) {
            _produtoService = produtoService;
        }

        // get api/produtos
        [HttpGet]
        public IActionResult GetAll() { 
            var produtos = _produtoService.ListAll();
            return Ok(produtos);
        }

        // Get api/produtos/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) {
            var produto = _produtoService.GetById(id);
            if (produto == null) {
                return NotFound();
            }
            return Ok(produto);
        }


    }
}
