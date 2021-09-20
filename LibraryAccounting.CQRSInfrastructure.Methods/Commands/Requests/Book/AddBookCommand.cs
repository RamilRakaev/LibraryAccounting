using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class AddBookCommand : IRequest<Book>
    {
        public Book Book { get; set; }
    }
}
