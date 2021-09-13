using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.EmailManagement
{
    public class MessageSending
    {
        public async Task SendEmailAsync(string email, string subject, string message, string recipientName = "")
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта LibraryAccounting", "librarianacc1@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(recipientName, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("librarianacc1@gmail.com", "fg36Kd2s");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
