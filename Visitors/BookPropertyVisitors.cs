using Domain.Interfaces;
using Domain.Model;

namespace Infrastructure.Visitors
{
    public class BookTitleVisitor : IVisitor<Book>
    {
        public string Title { get; set; }

        public BookTitleVisitor(string title)
        {
            Title = title;
        }

        public void Visit(Book element)
        {
            element.Title = Title;
        }
    }
    public class BookAuthorVisitor : IVisitor<Book>
    {
        public string Author { get; set; }

        public BookAuthorVisitor(string author)
        {
            Author = author;
        }

        public void Visit(Book element)
        {
            element.Title = Author;
        }
    }

    public class BookGenreVisitor : IVisitor<Book>
    {
        public string Genre { get; set; }

        public BookGenreVisitor(string genre)
        {
            Genre = genre;
        }

        public void Visit(Book element)
        {
            element.Title = Genre;
        }
    }

    public class BookPublisherVisitor : IVisitor<Book>
    {
        public string Publisher { get; set; }

        public BookPublisherVisitor(string publisher)
        {
            Publisher = publisher;
        }

        public void Visit(Book element)
        {
            element.Title = Publisher;
        }
    }

    public class BookAtuhorVisitor : BookPropertyVisitor, IVisitor<Book>
    {
        public BookAtuhorVisitor(string line) : base(line)
        { }

        protected override void Operation(Book book)
        {
            book.Author = Line;
        }
    }
}
