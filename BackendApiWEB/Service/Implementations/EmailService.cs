using System.Net;
using System.Net.Mail;
using BackendApiWEB.Service.Interfaces;

namespace BackendApiWEB.Service.Implementations {
    public class EmailService : IEmailService {
        public bool Enviar(string para, string assunto, string corpoHtml) {
            try {
                var remetente = "gilsonfonseca3000@gmail.com";
                var senha = "djvjclgcxeqlwbgy"; // senha de app do Gmail

                var smtp = new SmtpClient("smtp.gmail.com") {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(remetente, senha),
                    UseDefaultCredentials = false
                };

                var mail = new MailMessage(remetente, para, assunto, corpoHtml) {
                    IsBodyHtml = true
                };

                smtp.Send(mail);

                Console.WriteLine("EMAIL ENVIADO!");
                return true;
            } catch (Exception ex) {
                Console.WriteLine("ERRO EMAIL: " + ex.Message);
                return false;
            }
        }
    }
}
