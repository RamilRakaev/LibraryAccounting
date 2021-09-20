using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class RemoveBookingCommand : IRequest<Booking>
    {
        public int Id { get; set; }

        public RemoveBookingCommand(int id)
        {
            Id = id;
        }
    }
}
