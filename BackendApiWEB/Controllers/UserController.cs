using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // 1️⃣ LISTAR TODOS (com ou sem paginação)
        // GET api/users?page=1&pageSize=10
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var users = _service.GetPaged(page, pageSize);
            return Ok(users);
        }

        // 2️⃣ BUSCAR POR ID
        // GET api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _service.GetById(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(user);
        }
    }
}
