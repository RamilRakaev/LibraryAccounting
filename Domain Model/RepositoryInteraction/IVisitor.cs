
namespace Domain.Interfaces
{
    public interface IVisitor<Element>
    {
        public void Visit(Element element);
    }
}
