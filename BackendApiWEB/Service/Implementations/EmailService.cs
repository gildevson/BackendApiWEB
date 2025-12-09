using System.Net;
using System.Net.Mail;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service.Implementations
{
    public class EmailService : IEmailService
    {
        public bool Enviar(string para, string assunto, string corpoHtml)
        {
            try
            {
                var remetente = "gilsonfonseca3000@gmail.com";
                var senha = "uxovrzzxywlkbqfm";

                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 465,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(remetente, senha),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                var mail = new MailMessage(remetente, para, assunto, corpoHtml)
                {
                    IsBodyHtml = true
                };

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO EMAIL: " + ex.Message);
                return false;
            }
        }
    }
}
