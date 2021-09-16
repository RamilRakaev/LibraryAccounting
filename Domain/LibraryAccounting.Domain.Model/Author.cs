using LibraryAccounting.Domain.Interfaces.DataManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Domain.Model
{
    public class Author : IElement<Author>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
