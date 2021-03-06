using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetArmoredBooksHandler : IRequestHandler<GetArmoredBooksQuery, List<Book>>
    {
        private readonly IRepository<Book> _db;

        public GetArmoredBooksHandler(IRepository<Book> bookRepository)
        {
            _db = bookRepository;
        }

        public async Task<List<Book>> Handle(GetArmoredBooksQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(
                () => _db.GetAllAsNoTracking()
                .Where(b => b.Booking != null
                && b.Booking.ClientId == request.ClientId)
                .ToList());
        }
    }
}
