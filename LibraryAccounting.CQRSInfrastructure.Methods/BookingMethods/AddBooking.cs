using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods
{
    public class AddBookingCommand : IRequest<Booking>
    {
        public int BookId { get; set; }
        public int ClientId { get; set; }

        public AddBookingCommand(int bookId, int clientId)
        {
            BookId = bookId;
            ClientId = clientId;
        }
    }

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

        public class AddBookingValidator : AbstractValidator<AddBookingCommand>
        {
            public AddBookingValidator()
            {
                RuleFor(b => b.BookId).NotEqual(0);
                RuleFor(b => b.ClientId).NotEqual(0);
            }
        }
    }
}
