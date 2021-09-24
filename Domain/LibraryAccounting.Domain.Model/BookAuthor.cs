using LibraryAccounting.Domain.Interfaces.DataManagement;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.Model
{
    public class BookAuthor : IElement<BookAuthor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public BookAuthor()
        {

        }

        public BookAuthor(string name)
        {
            Name = name;
        }
    }
}
