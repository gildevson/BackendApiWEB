using BackendApiWEB.Models;

namespace BackendApiWEB.Service.Interfaces
{
    public interface IPermissaoService
    {
        IEnumerable<Permissao> GetByUsuario(Guid usuarioId);

    }
}
