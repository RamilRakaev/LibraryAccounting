using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods
{
    public class TransmissionAndAcceptanceBookCommand : IRequest<Booking>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public bool IsTransfer { get; set; }
    }

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
                booking = await _db.FindAsync(request.Id);
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

    public class TransmissionAndAcceptanceBookValidator : AbstractValidator<TransmissionAndAcceptanceBookCommand>
    {
        public TransmissionAndAcceptanceBookValidator()
        {
            RuleFor(b => b.Id).NotEqual(0).When(b => b.BookId == 0 && b.ClientId == 0);
            RuleFor(b => b.BookId).NotEqual(0).When(b => b.Id == 0);
            RuleFor(b => b.ClientId).NotEqual(0).When(b => b.Id == 0);
        }
    }
}
