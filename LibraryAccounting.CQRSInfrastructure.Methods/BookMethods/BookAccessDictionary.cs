using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookMethods
{
    public class BookAccessDictionaryQuery : IRequest<Dictionary<Book, bool>>
    {
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }
        public string Publisher { get; set; }

        public BookAccessDictionaryQuery()
        { }

        public BookAccessDictionaryQuery(int? genreId,
            int? authorId,
            string publisher)
        {
            GenreId = genreId;
            AuthorId = authorId;
            Publisher = publisher;
        }
    }

    public class BookAccessDictionaryHandler : IRequestHandler<BookAccessDictionaryQuery, Dictionary<Book, bool>>
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public BookAccessDictionaryHandler(IRepository<Book> bookRepository, IRepository<Booking> bookingRepository)
        {
            _bookRepository = bookRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<Dictionary<Book, bool>> Handle(BookAccessDictionaryQuery request, CancellationToken cancellationToken)
        {
            QueryFilter<Book, BookAccessDictionaryQuery> queryFilter =
                new QueryFilter<Book, BookAccessDictionaryQuery>(_bookRepository.GetAllAsNoTracking());
            var books = await queryFilter.FilterAsync(request);
            Dictionary<Book, bool> bookAccessDictionary = new Dictionary<Book, bool>();
            var bookings = _bookingRepository.GetAll();
            foreach (var book in books)
            {
                var isBooking = bookings.FirstOrDefault(b => b.BookId == book.Id) == null;
                bookAccessDictionary.Add(book, isBooking);
            }
            return bookAccessDictionary;
        }
    }
}