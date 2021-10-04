using LibraryAccounting.CQRSInfrastructure.TelegramMailingReceiving;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class TelegramCommand : IRequest<TelegramReceiving>
    {
        public ILogger Logger { get; set; }
        public string Token { get; set; }
    }
}
