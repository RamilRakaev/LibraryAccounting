using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class TransmissionAndAcceptanceBookHandler : IRequestHandler<TransmissionAndAcceptanceBookCommand, Booking>
    {
        private readonly IRepository<Booking> _db;
        private Booking booking;
        public TransmissionAndAcceptanceBookHandler(IRepository<Booking> db)
        {
            _db = db;
        }

        public async Task<Booking> Handle(TransmissionAndAcceptanceBookCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                booking = await _db.FindNoTrackingAsync(request.Id);
            }
            else
            {
                booking = await Task.Run(() => SearchBooking(request, _db));
            }

            if (booking != null)
            {
                booking = await Task.Run(() => UpdateBooking(request, booking));
                await _db.SaveAsync();
            }
            else
                throw new NullReferenceException("booking is not found");

            return booking;
        }

        static Booking UpdateBooking(TransmissionAndAcceptanceBookCommand request, Booking booking)
        {
            if (request.IsTransfer)
            {
                booking.IsTransmitted = true;
                booking.TransferDate = DateTime.Now;
            }
            else
            {
                booking.IsReturned = true;
                booking.ReturnDate = DateTime.Now;
            }
            return booking;
        }

        static Booking SearchBooking(TransmissionAndAcceptanceBookCommand request, IRepository<Booking> db)
        {
            return db.GetAll().FirstOrDefault(b => b.ClientId == request.ClientId && b.BookId == request.BookId);
        }
    }
}
