using System.Linq;
using System.Collections.Generic;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Handlers
{
    public class BooksByAuthorHandler : IRequestsHandlerComponent<Book>
    {
        readonly private Author _author;
        public BooksByAuthorHandler(Author author)
        {
            _author = author;
        }

        public void Handle(ref List<Book> elements)
        {
            if (_author != null)
                elements = elements.Where(b => b.Author == _author).ToList();
        }
    }

    public class BooksByGenreHandler : IRequestsHandlerComponent<Book>
    {
        readonly private Genre _genre;
        public BooksByGenreHandler(Genre genre)
        {
            _genre = genre;
        }

        public void Handle(ref List<Book> elements)
        {
            if (_genre != null)
                elements = elements.Where(b => b.Genre == _genre).ToList();
        }
    }

    public class BooksByPublisherHandler : IRequestsHandlerComponent<Book>
    {
        readonly private string _publisher;
        public BooksByPublisherHandler(string publisher)
        {
            _publisher = publisher;
        }

        public void Handle(ref List<Book> elements)
        {
            if (_publisher != "emtpy")
                elements = elements.
                    Where(b => b.Publisher == _publisher).
                    ToList();
        }
    }
    public class BookByTitleHandler : IReturningResultHandler<Book, Book>
    {
        private readonly string _title;
        public BookByTitleHandler(string title)
        {
            _title = title;
        }
        public Book Handle(IEnumerable<Book> elements)
        {
            return elements.FirstOrDefault(b => b.Title == _title);
        }
    }
}
