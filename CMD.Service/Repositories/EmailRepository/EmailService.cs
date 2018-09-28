using CMD.Model.Enumeradores;
using CMD.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.Repositories.EmailService
{
    public class EmailService : IEmailService
    {
        public bool EnviarEmail(string email, string username, long idMedida, bool condicao)
        {
            var fromAddress = new MailAddress("noreplyusercmd@gmail.com", "CmdWebApp");
            var toAddress = new MailAddress(email, username);
            const string fromPassword = "camaroamarello";
            string subject = $"CMD Web App - Medida Nº {idMedida} - {(condicao ? Helper.GetEnumDescription(Enums.StatusMedida.Aprovado) : Helper.GetEnumDescription(Enums.StatusMedida.Reprovado))}";
            string body = condicao ? $"A medida numero {idMedida} foi aprovada e está disponivel para impressão!" : $"A medida numero {idMedida} foi reprovada!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
                return true;
            }
        }
    }
}
