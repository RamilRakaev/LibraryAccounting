using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.CQRSInfrastructure.TelegramMailingReceiving;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.LibrarianPages
{
    public class TelegramBotModel : PageModel
    {
        private readonly IMediator _mediator;
        private ILogger<TelegramBotModel> _logger;

        public TelegramReceiving Telegram { get; set; }

        public TelegramBotModel(IMediator mediator, ILogger<TelegramBotModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task OnGet()
        {
        }

        public void OnGetStart()
        {
            Telegram = _mediator.Send(
                new TelegramCommand()
                {
                    Logger = _logger,
                    Token = "2006724722:AAEl8DPl_JuDmhXdDeAEEciguf73abZtEws"
                }).Result;
            Telegram.Start();
        }

        public async Task OnGetStop()
        {
            Telegram = await _mediator.Send(
                new TelegramCommand()
                {
                    Logger = _logger,
                    Token = "2006724722:AAEl8DPl_JuDmhXdDeAEEciguf73abZtEws"
                });
            Telegram.Stop();
        }
    }
}
