using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetBookHandler : BookHandler, IRequestHandler<GetBookQuery, Book>
    {
        public GetBookHandler(IRepository<Book> db) : base(db)
        { }

        public async Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            if (request.Id != 0)
            {
                return await _db.FindNoTrackingAsync(request.Id);
            }
            else
            {
                return _db.GetAll().
                    FirstOrDefault(b => b.Title == request.Title);
            }
        }
    }
}
