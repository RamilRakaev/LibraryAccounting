using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class RemoveAuthorCommand : IRequest<BookAuthor>
    {
        public int Id { get; set; }

        public RemoveAuthorCommand(int id)
        {
            Id = id;
        }
    }
}
