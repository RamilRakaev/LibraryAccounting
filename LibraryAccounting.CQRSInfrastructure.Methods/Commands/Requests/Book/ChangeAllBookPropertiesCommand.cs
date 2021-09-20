using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class ChangeAllBookPropertiesCommand : IRequest<Book>
    {
        public Book Book { get; set; }
        public ChangeAllBookPropertiesCommand(Book book)
        {
            Book = book;
        }
    }
}
