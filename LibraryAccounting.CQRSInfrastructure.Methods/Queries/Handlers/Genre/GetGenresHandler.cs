using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetGenresHandler : IRequestHandler<GetGenresQuery, IEnumerable<Genre>>
    {
        private readonly IRepository<Genre> _db;

        public GetGenresHandler(IRepository<Genre> db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            QueryFilter<Genre, GetGenresQuery> queryFilter =
                new QueryFilter<Genre, GetGenresQuery>(_db.GetAllAsNoTracking());
            return await queryFilter.FilterAsync(request);
        }
    }
}
