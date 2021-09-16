using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookMethods
{
    public class GetBooksQuery : IRequest<List<Book>>
    {
        public int? GenreId { get; set; }
        public string Publisher { get; set; }
        public int? AuthorId { get; set; }

        public GetBooksQuery()
        { }

        public GetBooksQuery(int? genreId,
            int? authorId,
            string publisher)
        {
            GenreId = genreId;
            AuthorId = authorId;
            Publisher = publisher;
        }
    }
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IRepository<Book> _db;

        public GetBooksQueryHandler(IRepository<Book> bookRepository)
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
