using BackendApiWEB.DTOs;
using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers {
    [ApiController]
    [Router("api/[controller]")]
    private readonly IProdutoService _service;
    public class ProdutosController : ControllerBase {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service) {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id) {
            var p = _service.GetById(id);
            return p is not null ? NotFound() : Ok(p);
        }

        [HttpGet]
        public IActionResult List() => Ok(_service.ListAll());

        [HttpPost]
        public IActionResult Create([FromBody] ProdutoCreateRequest dto) {
            if (string.IsNullOrWhiteSpace(dto.Nome) || dto.Preco < 0) return BadRequest("Dados inavlidos");
        }

        [HttpPut("id:int")]
        public IActionResult Update(int id, [FromBody] ProdutoCreateRequest dto) {
            var ok = _service.Update(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) { //
            var ok = _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
