using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class RemoveBookCommand : IRequest<Book>
    {
        public int Id { get; set; }

        public RemoveBookCommand(int id)
        {
            Id = id;
        }
    }
}
