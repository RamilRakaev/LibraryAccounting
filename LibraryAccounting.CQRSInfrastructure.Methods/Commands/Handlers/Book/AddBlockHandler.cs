using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class AddBlockHandler : BookHandler, IRequestHandler<AddBookCommand, Book>
    {
        public AddBlockHandler(IRepository<Book> db) : base(db)
        { }

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            await _db.AddAsync(request.Book);
            await _db.SaveAsync();
            return request.Book;
        }
    }
}
