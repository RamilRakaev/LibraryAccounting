using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace LibraryAccounting.Services.ToolInterfaces
{
    public interface ILibrarianTools
    {
        #region add and remove
        void RemoveBook(Book book);

        void AddBook(Book book);
        #endregion

        #region reception and delivery of books
        void AddReservation(Booking booking);

        void EditBooking(IVisitor<Booking> visitor, IReturningResultHandler<Booking, Booking> resultHandler);

        Booking GetBooking(IReturningResultHandler<Booking, Booking> requestsHandler);

        IEnumerable<Booking> GetBookings(IRequestsHandlerComponent<Booking> requestsHandler);

        IEnumerable<Booking> GetAllBookings();
        #endregion

        #region users
        User GetUser(int id);

        User GetUser(IReturningResultHandler<User, User> resultHandler);

        IEnumerable<User> GetUsers(IRequestsHandlerComponent<User> resultHandler);

        IEnumerable<User> GetAllUsers();
        #endregion

        #region books requests
        Book GetBook(int id);

        Book GetBook(IReturningResultHandler<Book, Book> resultHandler);

        IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetAllBooks();
        #endregion

        #region edit books
        void EditBook(IVisitor<Book> visitor, int id);

        void EditBooks(IVisitor<Book> visitor, IRequestsHandlerComponent<Book> handlerComponent = null);
        #endregion
    }
}
