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

        Booking GetBooking(IReturningResultHandler<Booking, Booking> requestsHandler);

        IEnumerable<Booking> GetBookings(IRequestsHandlerComponent<Booking> requestsHandler);

        IEnumerable<Booking> GetAllBookings();
        #endregion

        #region users
        ApplicationUser GetUser(int id);

        ApplicationUser GetUser(IReturningResultHandler<ApplicationUser, ApplicationUser> resultHandler);

        IEnumerable<ApplicationUser> GetUsers(IRequestsHandlerComponent<ApplicationUser> resultHandler);

        IEnumerable<ApplicationUser> GetAllUsers();
        #endregion

        #region books requests
        Book GetBook(int id);

        Book GetBook(IReturningResultHandler<Book, Book> resultHandler);

        IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetAllBooks();
        #endregion
    }
}
