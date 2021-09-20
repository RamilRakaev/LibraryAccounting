using System.Threading.Tasks;

namespace LibraryAccounting.Services.Mailing
{
    public interface IMessageSending
    {
        Task SendEmailAsync(
            string email,
            string subject,
            string message,
            string recipientName = "");
    }
}
