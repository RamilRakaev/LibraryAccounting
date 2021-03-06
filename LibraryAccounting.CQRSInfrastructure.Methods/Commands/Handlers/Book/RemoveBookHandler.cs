using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class RemoveBookHandler : BookHandler, IRequestHandler<RemoveBookCommand, Book>
    {
        public RemoveBookHandler(IRepository<Book> db) : base(db)
        { }

        public async Task<Book> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _db.FindNoTrackingAsync(request.Id);
            await _db.RemoveAsync(book);
            await _db.SaveAsync();
            return book;
        }
    }
}
