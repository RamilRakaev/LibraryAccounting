
namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IVisitor<Element>
    {
        public bool Visit(Element element);
    }
}
