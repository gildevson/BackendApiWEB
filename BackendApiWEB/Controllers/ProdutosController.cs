using BackendApiWEB.DTOs;
using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")] // 
        public IActionResult Get(Guid id)
        {
            var p = _service.GetById(id);
            return p is null ? NotFound() : Ok(p);
        }

        [HttpGet]
        public IActionResult List() => Ok(_service.ListAll());

        [HttpPost]
        public IActionResult Create([FromBody] ProdutoCreateRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || dto.Preco < 0)
                return BadRequest("Dados inválidos");

            var id = _service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id }, dto);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] ProdutoCreateRequest dto)
        {
            var ok = _service.Update(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var ok = _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}

