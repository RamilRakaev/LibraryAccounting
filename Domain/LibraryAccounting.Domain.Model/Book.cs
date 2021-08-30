using LibraryAccounting.Domain.Interfaces.DataManagement;

namespace LibraryAccounting.Domain.Model
{
    public class Book : IElement<Book>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }

        public bool Accept(IVisitor<Book> visitor)
        {
            if (visitor.GetType() == typeof(IVisitor<Book>))
                return visitor.Visit(this);
            else
                return false;
        }
    }
}
