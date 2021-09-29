using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetPublishersHandler : IRequestHandler<GetPublishersQuery, string[]>
    {
        private readonly IRepository<Book> _db;

        public GetPublishersHandler(IRepository<Book> bookRepository)
        {
            _db = bookRepository;
        }

        public async Task<string[]> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => 
            _db.GetAllAsNoTracking()
            .Select(b => b.Publisher)
            .Distinct()
            .ToArray());
        }
    }
}
