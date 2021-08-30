using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.ToolInterfaces
{
    public interface IClientTools
    {
        Book GetBook(int id);

        Book GetBook(IReturningResultHandler<Book, Book> resultHandler);

        IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetAllBooks();

        void AddReservation(Booking booking);

        void RemoveReservation(int Id);

        IEnumerable<Booking> GetBookings(IRequestsHandlerComponent<Booking> handlerComponent);

        IEnumerable<Booking> GetAllBookings();
    }
}
