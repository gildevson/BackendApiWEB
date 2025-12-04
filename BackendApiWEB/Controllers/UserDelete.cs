using BackendApiWEB.DTOs;
using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // DELETE api/auth
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteUserRequest dto)
        {
            var result = _authService.Delete(dto.Id);

            if (!result.Sucesso)
                return BadRequest(new { mensagem = result.Mensagem });

            return Ok(new { mensagem = "Usuário deletado com sucesso." });
        }
    }
}
