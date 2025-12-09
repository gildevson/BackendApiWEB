namespace BackendApiWEB.Service.Interfaces
{
    public interface IEmailService
    {
        bool Enviar(string para, string assunto, string corpoHtml);
    }
}
