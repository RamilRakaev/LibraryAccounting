using LibraryAccounting.Domain.Interfaces.DataManagement;

namespace LibraryAccounting.Domain.Model
{
    public class Book : IElement<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        public string Publisher { get; set; }
        public Booking Booking { get; set; }
    }
}
