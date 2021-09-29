
namespace LibraryAccounting.Services.Mailing
{
    public class EmailOptions
    {
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public string Password { get; set; }
    }
}
