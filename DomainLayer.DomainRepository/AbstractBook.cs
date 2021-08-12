using System;

namespace DomainLayer.DomainModel
{
    public abstract class AbstractBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
    }
}
