using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.CQRSInfrastructure.TelegramMailingReceiving;
using LibraryAccounting.Services.TelegramMailingReceiving;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class TelegramBotModel : PageModel
    {
        private ILogger<TelegramBotModel> _logger;

        private readonly AbstractTelegramReceiving _telegram;

        public TelegramBotModel(AbstractTelegramReceiving telegram, ILogger<TelegramBotModel> logger)
        {
            _telegram = telegram;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public void OnGetStart()
        {
            _telegram.Start();
        }

        public void OnGetStop()
        {
            _telegram.Stop();
        }
    }
}
