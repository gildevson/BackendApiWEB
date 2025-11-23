using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiWEB.Controllers {
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetUsers() {
            var users = _repo.GetAll()
                .Select(u => new UserResponse {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    DataCriacao = u.DataCriacao

                });

            return Ok(users);
        }
    }
}
