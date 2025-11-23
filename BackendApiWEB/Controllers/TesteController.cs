using BackendApiWEB.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers {
    [ApiController]
    [Route("api/teste")]
    public class TesteController : ControllerBase {
        private readonly DbContextDapper _dapper;

        public TesteController(DbContextDapper dapper) {
            _dapper = dapper;
        }

        [HttpGet("conexao")]
        public IActionResult Testar() {
            try {
                using var conn = _dapper.CreateConnection();
                conn.Open();
                return Ok("Conexão funcionando!");
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
