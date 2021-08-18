using Domain.Model;
using Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure.Handlers
{
    public class BooksByAuthor : HandlerComponent<Book>
    {
        readonly private string Author;
        public BooksByAuthor(string author)
        {
            Author = author;
        }

        public override IEnumerable<Book> Handle()
        {
            return Elements = Elements.Where(b => b.Author == Author);
        }
    }
}
