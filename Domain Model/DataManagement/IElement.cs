
namespace Domain.Interfaces
{
    public interface IElement
    {
        public int Id { get; set; }

        public void Accept(IVisitor<IElement> visitor)
        {
            visitor.Visit(this);
        }
    }
}
