using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods
{
    public class DeletingExpiredBooksCommand : IRequest<IEnumerable<Booking>>
    {
        public int MaxBookingPeriod { get; set; } = 3;
    }

    public class DeletingExpiredBooksHandler : IRequestHandler<DeletingExpiredBooksCommand, IEnumerable<Booking>>
    {
        private readonly IRepository<Booking> _bookings;

        public DeletingExpiredBooksHandler(IRepository<Booking> bookings)
        {
            _bookings = bookings;
        }

        public async Task<IEnumerable<Booking>> Handle(DeletingExpiredBooksCommand request, CancellationToken cancellationToken)
        {
            var bookings = await Task.Run(() => _bookings.GetAll().
               Where(b => (DateTime.Now - b.BookingDate).Days > request.MaxBookingPeriod));
            await _bookings.RemoveRangeAsync(bookings);
            await _bookings.SaveAsync();
            return bookings;
        }
    }
}
