using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;

namespace Infrastructure.Visitors
{
    public class BookTitleVisitor : IVisitor<Book>
    {
        public string Title { get; set; }

        public BookTitleVisitor(string title)
        {
            Title = title;
        }

        public bool Visit(Book element)
        {
            element.Title = Title;
            return true;
        }
    }
    public class BookAuthorVisitor : IVisitor<Book>
    {
        public string Author { get; set; }

        public BookAuthorVisitor(string author)
        {
            Author = author;
        }

        public bool Visit(Book element)
        {
            element.Author = Author;
            return true;
        }
    }

    public class BookGenreVisitor : IVisitor<Book>
    {
        public string Genre { get; set; }

        public BookGenreVisitor(string genre)
        {
            Genre = genre;
        }

        public bool Visit(Book element)
        {
            element.Genre = Genre;
            return true;
        }
    }

    public class BookPublisherVisitor : IVisitor<Book>
    {
        public string Publisher { get; set; }

        public BookPublisherVisitor(string publisher)
        {
            Publisher = publisher;
        }

        public bool Visit(Book element)
        {
            element.Publisher = Publisher;
            return true;
        }
    }
}
