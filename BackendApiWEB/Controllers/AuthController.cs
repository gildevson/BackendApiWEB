using BackendApiWEB.DTOs;
using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IAuthService _auth;

        // 🔥 FALTAVA ESTE CONSTRUTOR
        public AuthController(IAuthService auth) {
            _auth = auth;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest dto) {
            var result = _auth.Login(dto);

            if (!result.Sucesso)
                return BadRequest(new { mensagem = result.Mensagem });

            return Ok(result);
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] RegistrarRequest dto) {
            var result = _auth.Registrar(dto);

            if (!result.Sucesso)
                return BadRequest(new { mensagem = result.Mensagem });

            return Ok(result);
        }
    }
}
