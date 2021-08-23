using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Handlers
{
    public class BookingsByClientIdHandler : IRequestsHandlerComponent<Booking>
    {
        readonly private int BookId;

        public BookingsByClientIdHandler(int bookId)
        {
            BookId = bookId;
        }

        public void Handle(ref IEnumerable<Booking> elements)
        {
            elements = elements.Where(b => !b.IsReturned && b.IsTransmitted && b.BookId == BookId);
            if (elements.Count() != 1)
                throw new Exception("one book cannot be booked twice");
        }
    }
}
