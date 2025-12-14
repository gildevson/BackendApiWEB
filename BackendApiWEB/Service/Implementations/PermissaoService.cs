using BackendApiWEB.Data.Interfaces;
using BackendApiWEB.Models;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service {
    public class PermissaoService : IPermissaoService {
        private readonly IPermissaoRepository _repo;

        public PermissaoService(IPermissaoRepository repo) {
            _repo = repo;
        }

        public IEnumerable<Permissao> GetByUsuario(Guid usuarioId) {
            return _repo.GetByUsuario(usuarioId);
        }
    }
}
