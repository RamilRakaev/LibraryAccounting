using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookMethods
{
    public class RemoveBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
    }

    public class RemoveBookHandler : IRequestHandler<RemoveBookCommand, Book>
    {
        private readonly IRepository<Book> _db;
        public RemoveBookHandler(IRepository<Book> db)
        {
            _db = db;
        }

        public async Task<Book> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.FindAsync(request.Id);
            _db.Remove(book);
            await _db.SaveAsync();
            return book;
        }
    }

    public class RemoveBookValidator : AbstractValidator<RemoveBookCommand>
    {
        public RemoveBookValidator()
        {
            RuleFor(b => b.Id).NotEqual(0);
        }
    }
}
