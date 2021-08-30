using Infrastructure.Handlers;
using LibraryAccounting.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class HandlersTest
    {
        List<Book> Books = new List<Book>()
            {
                new Book(){ Id = 1, Title = "book1", Author = "author1", Genre = "genre2", Publisher = "publisher1"},
                new Book(){ Id = 2, Title = "book2", Author = "author1", Genre = "genre4", Publisher = "publisher1"},
                new Book(){ Id = 3, Title = "book3", Author = "author5", Genre = "genre2", Publisher = "publisher4"},
                new Book(){ Id = 4, Title = "book4", Author = "author4", Genre = "genre4", Publisher = "publisher4"}
            };
        List<Booking> Bookings = new List<Booking>()
            {
                new Booking(){ Id = 1, BookId = 1, ClientId = 1, BookingDate = new DateTime(2021, 8, 21)},
                new Booking(){ Id = 2, BookId = 2, ClientId = 2, BookingDate = DateTime.Now,
                    IsTransmitted = true, TransferDate = DateTime.Now},
                new Booking(){ Id = 3, BookId = 3, ClientId = 2, BookingDate = DateTime.Now,
                    IsTransmitted = true, TransferDate = DateTime.Now, IsReturned = true, ReturnDate = DateTime.Now}
            };
        List<Booking> BookingOutput;
        List<Book> booksOutput;

        [TestMethod]
        public void BooksByPropertyHandlers()
        {
            var byAuthor = new BooksByAuthorHandler("author1");
            booksOutput = new List<Book>(Books);
            byAuthor.Handle(ref booksOutput);
            Assert.AreEqual(booksOutput.Count, 2);

            var byTitle = new BookByTitleHandler("book1");
            Assert.AreEqual(Books[0], byTitle.Handle(Books));
        }

        [TestMethod]
        public void BookingBooksHandlers()
        {
            BookingOutput = new List<Booking>(Bookings);
            var byClientId = new BookingsByClientIdHandler(2);
            byClientId.Handle(ref BookingOutput);
            Assert.AreEqual(BookingOutput.Count, 1);

            booksOutput = new List<Book>(Books);
            var bokingBooks = new BookingBooksHandler(Bookings);
            bokingBooks.Handle(ref booksOutput);
            Assert.AreEqual(booksOutput.Count, 3);

            BookingOutput = new List<Booking>(Bookings);
            var byBookingDate = new SortingByBookingDateHandler();
            byBookingDate.Handle(ref BookingOutput);
            Assert.AreEqual(BookingOutput[2].BookingDate, new DateTime(2021, 8, 21));

            var expiredBooksId = new ExpiredBooksIdHandler(3);
            Assert.AreEqual(expiredBooksId.Handle(Bookings).Length, 1);

            var bookingByBookId = new BookingByBookIdHandler(1);
            Assert.AreEqual(bookingByBookId.Handle(Bookings), Bookings[0]);
        }
    }
}
