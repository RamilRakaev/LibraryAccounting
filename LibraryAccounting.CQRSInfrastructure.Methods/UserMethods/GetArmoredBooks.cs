using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class GetArmoredBooksQuery : IRequest<Dictionary<Booking, Book>>
    {
        public int CliendId { get; set; }
    }

    public class GetArmoredBooksHandler : IRequestHandler<GetArmoredBooksQuery, Dictionary<Booking, Book>>
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly Dictionary<Booking, Book> dictionary;

        public GetArmoredBooksHandler(IRepository<Book> bookRepository, IRepository<Booking> bookingRepository)
        {
            _bookRepository = bookRepository;
            _bookingRepository = bookingRepository;
            dictionary = new Dictionary<Booking, Book>();
        }

        public async Task<Dictionary<Booking, Book>> Handle(GetArmoredBooksQuery request, CancellationToken cancellationToken)
        {
            var value = await Task.Run(() =>
            _bookRepository.GetAll()
                .Join(_bookingRepository.GetAll()
                .Where(b => b.ClientId == request.CliendId),
                book => book.Id,
                booking => booking.BookId,
                (book, booking) => new
                {
                    Book = book,
                    Booking = booking
                }
                ));
            foreach (var pair in value)
            {
                dictionary.Add(pair.Booking, pair.Book);
            }
            return dictionary;
        }

    }
}
