
namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IElement<Element>
    {
        public int Id { get; set; }
    }

    public interface IElement : IElement<IElement>
    {

    }
}
