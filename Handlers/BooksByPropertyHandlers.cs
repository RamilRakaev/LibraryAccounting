using System.Linq;
using System.Collections.Generic;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;

namespace Infrastructure.Handlers
{
    public class BooksByAuthorHandler : IRequestsHandlerComponent<Book>
    {
        readonly private string Author;
        public BooksByAuthorHandler(string author)
        {
            Author = author;
        }

        public void Handle(ref List<Book> elements)
        {
            elements = elements.Where(b => b.Author == Author).ToList();
        }
    }

    public class BooksByGenreHandler : IRequestsHandlerComponent<Book>
    {
        readonly private string Genre;
        public BooksByGenreHandler(string genre)
        {
            Genre = genre;
        }

        public void Handle(ref List<Book> elements)
        {
            elements = elements.Where(b => b.Genre == Genre).ToList();
        }
    }

    public class BooksByPublisherHandler : IRequestsHandlerComponent<Book>
    {
        readonly private string Publisher;
        public BooksByPublisherHandler(string genre)
        {
            Publisher = genre;
        }

        public void Handle(ref List<Book> elements)
        {
            elements = elements.Where(b => b.Publisher == Publisher).ToList();
        }
    }
    public class BookByTitleHandler : IReturningResultHandler<Book, Book>
    {
        private string Title;
        public BookByTitleHandler(string title)
        {
            Title = title;
        }
        public Book Handle(IEnumerable<Book> elements)
        {
            return elements.FirstOrDefault(b => b.Title == Title);
        }
    }
}
