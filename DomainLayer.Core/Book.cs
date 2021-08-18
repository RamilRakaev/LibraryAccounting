using Domain.Interfaces;

namespace Domain.Model
{
    public class Book: IElement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }

    }
}
