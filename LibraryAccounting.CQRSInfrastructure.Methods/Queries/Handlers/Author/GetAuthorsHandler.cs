using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<BookAuthor>>
    {
        private readonly IRepository<BookAuthor> _db;

        public GetAuthorsHandler(IRepository<BookAuthor> db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BookAuthor>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            QueryFilter<BookAuthor, GetAuthorsQuery> queryFilter =
                new QueryFilter<BookAuthor, GetAuthorsQuery>(_db.GetAllAsNoTracking());
            return await queryFilter.FilterAsync(request);
        }
    }
}
