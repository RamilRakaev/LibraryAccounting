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
        public Book Book { get; set; }
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
            await db.AddAsync(request.Book);
            await db.SaveAsync();
            return request.Book;
        }
    }

    public class AddBookValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookValidator()
        {
            RuleFor(b => b.Book.Id).Equals(0);
        }
    }

}
