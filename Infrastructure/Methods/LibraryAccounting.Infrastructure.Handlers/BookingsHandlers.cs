using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Handlers
{
    public class BookingsByClientIdHandler : IRequestsHandlerComponent<Booking>
    {
        readonly private int _clientId;

        public BookingsByClientIdHandler(int clientId)
        {
            _clientId = clientId;
        }

        public void Handle(ref List<Booking> elements)
        {
            elements = elements.
                Where(b => !b.IsReturned && b.ClientId == _clientId).
                ToList();
        }
    }

    public class BookingBooksHandler : IRequestsHandlerComponent<Book>
    {
        readonly private IEnumerable<Booking> Bookings;

        public BookingBooksHandler(IEnumerable<Booking> bookings)
        {
            Bookings = bookings;
        }

        public void Handle(ref List<Book> elements)
        {
            var booksId = Bookings.Select(b => b.BookId);
            elements = elements.Where(b => booksId.Contains(b.Id)).ToList();
        }
    }

    public class SortingByBookingDateHandler : IRequestsHandlerComponent<Booking>
    {
        public void Handle(ref List<Booking> elements)
        {
            elements = elements.OrderByDescending(b => b.BookingDate).ToList();
        }
    }

    public class ExpiredBooksIdHandler : IReturningResultHandler<Booking[], Booking>
    {
        private int _maxBookingPeriod;

        public ExpiredBooksIdHandler(int maxBookingPeriod)
        {
            _maxBookingPeriod = maxBookingPeriod;
        }

        public Booking[] Handle(IEnumerable<Booking> elements)
        {
            return elements.
                Where(b => (DateTime.Now - b.BookingDate).Days > _maxBookingPeriod).
                ToArray();
        }
    }

    public class BookingByBookIdHandler : IReturningResultHandler<Booking, Booking>
    {
        readonly private int _bookId;

        public BookingByBookIdHandler(int bookId)
        {
            _bookId = bookId;
        }

        public Booking Handle(IEnumerable<Booking> elements)
        {
            return elements.FirstOrDefault(b => !b.IsReturned && b.BookId == _bookId);
        }
    }

    public class BookCatalogHandler : IReturningResultHandler<Dictionary<Book, bool>, Book, Booking>
    {
        public Dictionary<Book, bool> Handle(IEnumerable<Book> books, IEnumerable<Booking> bookings)
        {
            Dictionary<Book, bool> valuePairs = new Dictionary<Book, bool>();
            var bookingsId = bookings.Where(b => !b.IsReturned).Select(b => b.BookId).ToArray();
            for (int i = 0; i < books.Count(); i++)
            {
                valuePairs.Add(books.ElementAt(i), !bookingsId.Contains(books.ElementAt(i).Id));
            }
            return valuePairs;
        }
    }

    public class UsersWidthIdHandler : IReturningResultHandler<Dictionary<int, ApplicationUser>, ApplicationUser>
    {
        public Dictionary<int, ApplicationUser> Handle(IEnumerable<ApplicationUser> elements)
        {
            var dictionary = new Dictionary<int, ApplicationUser>();
            foreach(var elem in elements)
            {
                dictionary.Add(elem.Id, elem);
            }
            return dictionary;
        }
    }

    public class BookingsWidthBookIdHandler : IReturningResultHandler<Dictionary<int, Booking>, Booking>
    {
        public Dictionary<int, Booking> Handle(IEnumerable<Booking> elements)
        {
            var dictionary = new Dictionary<int, Booking>();
            foreach(var elem in elements)
            {
                dictionary.Add(elem.BookId, elem);
            }
            return dictionary;
        }
    }
}
