using LibraryAccounting.Domain.Interfaces.DataManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Domain.Model
{
    public class Genre : IElement<Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

        public Genre()
        {

        }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
