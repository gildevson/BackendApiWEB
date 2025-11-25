using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.DTOs;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        // ===============================
        // PAGINAÇÃO
        // ===============================
        public PaginatedResult<UserResponse> GetPaged(int page, int pageSize)
        {
            var data = _repo.GetPaged(page, pageSize);
            var total = _repo.Count();

            return new PaginatedResult<UserResponse>
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Data = data.Select(x => new UserResponse
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    DataCriacao = x.DataCriacao
                })
            };
        }

        // ===============================
        // BUSCAR POR ID
        // ===============================
        public UserResponse GetById(Guid id)
        {
            var user = _repo.GetById(id);

            if (user == null)
                return null!;

            return new UserResponse
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                DataCriacao = user.DataCriacao
            };
        }

        // ===============================
        // CRIAR USUÁRIO
        // ===============================
        public AuthResult Create(UsuarioCreateDTO dto)
        {
            var existente = _repo.GetByEmail(dto.Email);

            if (existente != null)
                return new AuthResult(false, "E-mail já está em uso.", null);

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                DataCriacao = DateTime.Now
            };

            _repo.Create(usuario);

            var userResponse = new UserResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao
            };

            return new AuthResult(true, "Usuário criado com sucesso.", userResponse);
        }

        // ===============================
        // DELETAR
        // ===============================
        public bool Delete(Guid id)
        {
            return _repo.Delete(id);
        }

        // ===============================
        // ATUALIZAR
        // ===============================
        public bool Update(Guid id, UsuarioCreateDTO dto)
        {
            return _repo.Update(id, dto);
        }
    }
}
