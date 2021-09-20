using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class TransmissionAndAcceptanceBookCommand : IRequest<Booking>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public bool IsTransfer { get; set; }
    }
}
