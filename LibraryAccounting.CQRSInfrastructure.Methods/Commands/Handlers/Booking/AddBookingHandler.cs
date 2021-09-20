using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class AddBookingHandler : IRequestHandler<AddBookingCommand, Booking>
    {
        private readonly IRepository<Booking> _db;
        public AddBookingHandler(IRepository<Booking> db)
        {
            _db = db;
        }

        public async Task<Booking> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking(request.BookId, request.ClientId);
            await _db.AddAsync(booking);
            await _db.SaveAsync();
            return booking;
        }
    }
}
