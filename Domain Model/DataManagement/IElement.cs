
namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IElement<Element>
    {
        public int Id { get; set; }
        public bool Accept(IVisitor<Element> visitor);
    }

    public interface IElement : IElement<IElement>
    {
    }
}
