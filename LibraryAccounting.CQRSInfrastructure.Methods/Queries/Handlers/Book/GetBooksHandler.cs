using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetBooksHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IRepository<Book> _db;

        public GetBooksHandler(IRepository<Book> bookRepository)
        {
            _db = bookRepository;
        }

        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            QueryFilter<Book, GetBooksQuery> queryFilter =
                new QueryFilter<Book, GetBooksQuery>(_db.GetAllAsNoTracking());
            return await queryFilter.FilterAsync(request);
        }
    }
}
