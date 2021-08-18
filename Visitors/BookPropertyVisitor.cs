using Domain.Model;

namespace Infrastructure.Visitors
{
    public abstract class BookPropertyVisitor
    {
        readonly protected string Line;

        public BookPropertyVisitor(string line)
        {
            Line = line;
        }

        public bool Visit(Book element)
        {
            Operation(element);
        }

        abstract protected void Operation(Book book);
    }
}
