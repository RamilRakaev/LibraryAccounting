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
    public class GiveBookToClientCommand : IRequest<Booking>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ClientId { get; set; }
        public bool GetToClient { get; set; }
    }

    public class GiveBookToClientHandler : IRequestHandler<GiveBookToClientCommand, Booking>
    {
        private readonly IRepository<Booking> _db;
        private Booking booking;
        public GiveBookToClientHandler(IRepository<Booking> db)
        {
            _db = db;
        }

        public async Task<Booking> Handle(GiveBookToClientCommand request, CancellationToken cancellationToken)
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
            }
            else
                throw new NullReferenceException("booking is not found");

            return booking;
        }

        static Booking UpdateBooking(GiveBookToClientCommand request, Booking booking)
        {
            if (request.GetToClient)
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

        static Booking SearchBooking(GiveBookToClientCommand request, IRepository<Booking> db)
        {
            return db.GetAll().FirstOrDefault(b => b.ClientId == request.ClientId && b.BookId == request.BookId);
        }
    }

    public class GiveBookToClientValidator : AbstractValidator<GiveBookToClientCommand>
    {
        public GiveBookToClientValidator()
        {
            RuleFor(b => b.Id).NotEqual(0).When(b => b.BookId == 0 && b.ClientId == 0);
            RuleFor(b => b.BookId).NotEqual(0).When(b => b.Id == 0);
            RuleFor(b => b.ClientId).NotEqual(0).When(b => b.Id == 0);
        }
    }
}
