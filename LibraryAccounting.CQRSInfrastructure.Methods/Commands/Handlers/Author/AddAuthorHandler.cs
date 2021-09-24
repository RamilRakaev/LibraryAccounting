using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, BookAuthor>
    {
        private readonly IRepository<BookAuthor> _db;

        public AddAuthorHandler(IRepository<BookAuthor> db)
        {
            _db = db;
        }
        public async Task<BookAuthor> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new BookAuthor(request.Name);
            await _db.AddAsync(author);
            await _db.SaveAsync();
            return author;
        }
    }
}
