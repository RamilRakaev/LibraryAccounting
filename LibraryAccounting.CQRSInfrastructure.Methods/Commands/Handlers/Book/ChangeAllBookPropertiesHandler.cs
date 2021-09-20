using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class ChangeAllBookPropertiesHandler : BookHandler, IRequestHandler<ChangeAllBookPropertiesCommand, Book>
    {
        public ChangeAllBookPropertiesHandler(IRepository<Book> db) : base(db)
        { }

        public async Task<Book> Handle(ChangeAllBookPropertiesCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.FindAsync(request.Book.Id);
            if (book == null)
            {
                throw new NullReferenceException("The book with the given id was not found");
            }
            book.Title = request.Book.Title;
            book.AuthorId = request.Book.AuthorId;
            book.Author = request.Book.Author;
            book.GenreId = request.Book.GenreId;
            book.Genre = request.Book.Genre;
            book.Publisher = request.Book.Publisher;
            await _db.SaveAsync();
            return book;
        }
    }
}
