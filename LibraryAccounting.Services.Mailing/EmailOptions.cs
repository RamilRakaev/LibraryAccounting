
namespace LibraryAccounting.Services.Mailing
{
    public class EmailOptions
    {
        public string SenderName { get; set; } = "Администрация сайта LibraryAccounting";
        public string SenderAddress { get; set; } = "librarianacc1@gmail.com";
        public string SMTP { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 465;
        public bool UseSSL { get; set; } = true;
        public string Password { get; set; } = "fg36Kd2s";
    }
}
