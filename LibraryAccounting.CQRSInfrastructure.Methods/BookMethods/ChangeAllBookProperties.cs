using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookMethods
{
    public class ChangeAllBookPropertiesCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public Book Book { get; set; }
    }

    public class ChangeAllBookPropertiesHandler : IRequestHandler<ChangeAllBookPropertiesCommand, Book>
    {
        private readonly IRepository<Book> _db;
        public ChangeAllBookPropertiesHandler(IRepository<Book> db)
        {
            _db = db;
        }

        public async Task<Book> Handle(ChangeAllBookPropertiesCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.FindAsync(request.Id);
            if(book == null)
            {
                throw new NullReferenceException("The book with the given id was not found");
            }
            book.Title = request.Book.Title;
            book.Author = request.Book.Author;
            book.Genre = request.Book.Genre;
            book.Publisher = request.Book.Publisher;
            await _db.SaveAsync();
            return book;
        }
    }
}
