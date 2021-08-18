
namespace Domain.Interfaces
{
    public interface IElement
    {
        public int Id { get; set; }

        public bool Accept(IVisitor<IElement> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
