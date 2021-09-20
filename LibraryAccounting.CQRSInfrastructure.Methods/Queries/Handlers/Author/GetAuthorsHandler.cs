using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<Author>>
    {
        private readonly IRepository<Author> _db;

        public GetAuthorsHandler(IRepository<Author> db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            QueryFilter<Author, GetAuthorsQuery> queryFilter =
                new QueryFilter<Author, GetAuthorsQuery>(_db.GetAllAsNoTracking());
            return await queryFilter.FilterAsync(request);
        }
    }
}
