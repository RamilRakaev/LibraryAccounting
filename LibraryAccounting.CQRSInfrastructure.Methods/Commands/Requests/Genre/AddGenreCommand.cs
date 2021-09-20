using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class AddGenreCommand : IRequest<Genre>
    {
        public string Name { get; set; }

        public AddGenreCommand(string name)
        {
            Name = name;
        }
    }
}
