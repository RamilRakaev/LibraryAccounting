using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class RemoveAuthorHandler : IRequestHandler<RemoveAuthorCommand, BookAuthor>
    {
        private readonly IRepository<BookAuthor> _db;

        public RemoveAuthorHandler(IRepository<BookAuthor> db)
        {
            _db = db;
        }

        public async Task<BookAuthor> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            var Author = await _db.FindNoTrackingAsync(request.Id);
            await _db.RemoveAsync(Author);
            return Author;
        }
    }
}
