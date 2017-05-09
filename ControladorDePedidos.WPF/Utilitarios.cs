using ControladorDePedidos.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ControladorDePedidos.WPF
{
    public static class Utilitarios
    {
        public static void EnviarEmail(string destinatario, string titulo, string mensagem)
        {
            var emailDeOrigem = "dssystem@outlook.com.br";
            var servidorSMTP = "in-v3.mailjet.com";
            var portaSmtp = 587;
            var usuarioSMTP = "3ac1478ecf3a0b5b9c5c38ceea62a491";
            var senhaSMTP = "f23da313f91863760e946dc6028dc0a6";

            var smtp = new SmtpClient();
            smtp.Host = servidorSMTP;
            smtp.Port = portaSmtp;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(usuarioSMTP, senhaSMTP);


            var msg = new MailMessage();
            msg.To.Add(destinatario);
            msg.Subject = titulo;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emailDeOrigem);
            msg.ReplyToList.Add(emailDeOrigem);
            msg.Body = mensagem;


            smtp.Send(msg);

        }
    }
}
