using LibraryAccounting.Services.Mailing;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Mailing
{
    public class MessageSending : IMessageSending
    {
        private readonly EmailOptions _options;

        public MessageSending(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string message,
            string recipientName = "")
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(
                new MailboxAddress(_options.SenderName,
                _options.SenderAddress)
                );
            emailMessage.To.Add(new MailboxAddress(recipientName, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using var client = new SmtpClient();
            await client.ConnectAsync(_options.SMTP, _options.Port, _options.UseSSL);
            await client.AuthenticateAsync(_options.SenderAddress, _options.Password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
