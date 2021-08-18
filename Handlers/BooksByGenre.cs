using Domain.Model;
using Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure.Handlers
{
    public class BooksByGenre : HandlerComponent<Book>
    {
        readonly private string Genre;
        public BooksByGenre(string genre)
        {
            Genre = genre;
        }

        public override IEnumerable<Book> Handle()
        {
            return Elements = Elements.Where(b => b.Genre == Genre);
        }
    }
}
