using BackendApiWEB.DTOs;
using BackendApiWEB.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers {
    [ApiController]
    [Route("auth/reset")]
    public class ResetSenhaController : ControllerBase {
        private readonly IAuthService _auth;

        public ResetSenhaController(IAuthService auth) {
            _auth = auth;
        }

        // ============================
        // SOLICITAR RESET DE SENHA
        // ============================
        [HttpPost("solicitar")]
        public IActionResult Solicitar([FromBody] ResetSenhaSolicitarRequest dto) {
            var result = _auth.SolicitarResetSenha(dto);

            if (!result.Sucesso)
                return BadRequest(new { mensagem = result.Mensagem });

            return Ok(new { mensagem = result.Mensagem });
        }

        // ============================
        // VALIDAR TOKEN
        // ============================
        [HttpGet("validar")]
        public IActionResult Validar([FromQuery] string token) {
            var result = _auth.ValidarToken(token);

            if (!result.Sucesso)
                return BadRequest(new { mensagem = result.Mensagem });

            return Ok(new { mensagem = result.Mensagem });
        }

        // ============================
        // RESETAR SENHA
        // ============================
        [HttpPost("resetar")]
        public IActionResult Resetar([FromBody] ResetSenhaRequest dto) {
            var result = _auth.ResetarSenha(dto);

            if (!result.Sucesso)
                return BadRequest(new { mensagem = result.Mensagem });

            return Ok(new { mensagem = "Senha alterada com sucesso!" });
        }
    }
}
