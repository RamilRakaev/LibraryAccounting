using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.ToolsInterfaces
{
    public interface ILibrarianTools
    {
        #region add and remove
        void RemoveBook(Book book);
        void AddBook(Book book);
        #endregion

        #region reception and delivery of books
        bool GiveBookToClient(int bookId, int clientId);
        bool GetBookFromClient(int bookId, int clientId);
        #endregion

        #region books requests
        Book GetBook(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetAllBooks();
        #endregion

        #region edit books
        void EditBook(IVisitor<Book> visitor, int id);

        void EditBooks(IVisitor<Book> visitor, IRequestsHandlerComponent<Book> handlerComponent = null);
        #endregion
    }
}
