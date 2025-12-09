namespace BackendApiWEB.Data.Interfaces
{
    public interface IResetSenhaRepository
    {
        void CriarToken(Guid usuarioId, Guid token, DateTime expiracao);
        bool TokenValido(string token);
        Guid? ObterUsuarioPorToken(string token);
        void MarcarTokenComoUsado(string token);
    }
}
