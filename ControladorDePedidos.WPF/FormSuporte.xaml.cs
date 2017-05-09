using ControladorDePedidos.Modelo;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace ControladorDePedidos.WPF
{

    public partial class FormSuporte : Window
    {
        public FormSuporte()
        {
            InitializeComponent();
        }

        private void btnEnviarEmail_Click(object sender, RoutedEventArgs e)
        {
            //var Usuario = new Usuario();
            //Usuario.Email = txtRemetente.Text;
                        var emailRemetente = txtRemetente.Text;
                        //var EmailSuporte = txtEmail.Text; // suporte do sistema
            var assuntoEmail = txtAssunto.Text; //assunto do e-mail
            var Descricao = txtDescriçãoEmail.Text; //mensagem do sistema
            
            var emailOriginal = "dssystem@outlook.com.br";
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
            msg.To.Add(emailRemetente);
            msg.Subject = assuntoEmail;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emailOriginal);
            msg.ReplyToList.Add(emailOriginal);
            msg.Body = Descricao;           
          
            smtp.Send(msg);
            MessageBox.Show("E-mail enviado com sucesso!");
           this.Close();
        }
    }
}
