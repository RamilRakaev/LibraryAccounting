using MediatR;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class AddAuthorCommand : IRequest<BookAuthor>
    {
        public string Name { get; set; }

        public AddAuthorCommand(string name)
        {
            Name = name;
        }
    }
}
