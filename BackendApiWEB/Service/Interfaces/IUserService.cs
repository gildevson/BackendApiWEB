using BackendApiWEB.DTOs;

namespace BackendApiWEB.Service.Interfaces
{
    public interface IUserService
    {
        PaginatedResult<UserResponse> GetPaged(int page, int pageSize);

        UserResponse? GetById(Guid id);

        AuthResult Create(UsuarioCreateDTO dto);

        bool Delete(Guid id);

        bool Update(Guid id, UsuarioCreateDTO dto);
    }
}
