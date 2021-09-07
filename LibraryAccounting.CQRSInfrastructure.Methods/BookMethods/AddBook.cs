using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookMethods
{
    public class AddBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
    }

    public class AddBlockHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IRepository<Book> db;
        public AddBlockHandler(IRepository<Book> _db)
        {
            db = _db;
        }

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            Book book = new Book() { Title = request.Title, Author = request.Author, Genre = request.Genre, Publisher = request.Publisher };
            await db.AddAsync(book);
            await db.SaveAsync();
            return book;
        }
    }

    public class AddBookValidator : AbstractValidator<Book>
    {
        public AddBookValidator()
        {
            RuleFor(b => b.Id).Equals(0);
            RuleFor(b => b.Title).NotEmpty();
            RuleFor(b => b.Author).NotEmpty();
            RuleFor(b => b.Genre).NotEmpty();
            RuleFor(b => b.Publisher).NotEmpty();
        }
    }

}
