using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class RemoveGenreCommand : IRequest<Genre>
    {
        public int Id { get; set; }

        public RemoveGenreCommand(int id)
        {
            Id = id;
        }
    }
}