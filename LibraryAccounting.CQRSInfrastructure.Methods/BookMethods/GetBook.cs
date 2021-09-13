using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookMethods
{
    public class GetBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class GetBookHandler : BookHandler, IRequestHandler<GetBookCommand, Book>
    {
        public GetBookHandler(IRepository<Book> db) : base(db)
        { }

        public async Task<Book> Handle(GetBookCommand request, CancellationToken cancellationToken)
        {
            if(request.Id != 0)
            {
                return await _db.FindAsync(request.Id);
            }
            else
            {
                return _db.GetAll().
                    FirstOrDefault(b => b.Title == request.Title);
            }
        }
    }
}
