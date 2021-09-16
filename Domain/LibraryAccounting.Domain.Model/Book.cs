using LibraryAccounting.Domain.Interfaces.DataManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAccounting.Domain.Model
{
    public class Book : IElement<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        public string Publisher { get; set; }
        public Booking Booking { get; set; }
    }
}
