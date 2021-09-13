using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Visitors
{
    public class BookTitleVisitor : IVisitor<Book>
    {
        private readonly string _title;

        public BookTitleVisitor(string title)
        {
            _title = title;
        }

        public bool Visit(Book element)
        {
            element.Title = _title;
            return true;
        }
    }
    public class BookAuthorVisitor : IVisitor<Book>
    {
        private readonly string _author;

        public BookAuthorVisitor(string author)
        {
            _author = author;
        }

        public bool Visit(Book element)
        {
            element.Author = _author;
            return true;
        }
    }

    public class BookGenreVisitor : IVisitor<Book>
    {
        private readonly string _genre;

        public BookGenreVisitor(string genre)
        {
            _genre = genre;
        }

        public bool Visit(Book element)
        {
            element.Genre = _genre;
            return true;
        }
    }

    public class BookPublisherVisitor : IVisitor<Book>
    {
        private readonly string _publisher;

        public BookPublisherVisitor(string publisher)
        {
            _publisher = publisher;
        }

        public bool Visit(Book element)
        {
            element.Publisher = _publisher;
            return true;
        }
    }
}
