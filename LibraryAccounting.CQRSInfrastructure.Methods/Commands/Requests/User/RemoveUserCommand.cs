using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class RemoveUserCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
    }
}
