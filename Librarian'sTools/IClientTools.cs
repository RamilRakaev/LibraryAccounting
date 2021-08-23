using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.ToolsInterfaces
{
    public interface IClientTools
    {
        Book GetBook(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent);

        IEnumerable<Book> GetAllBooks();

        bool BorrowBook(int BookId);
    }
}
