using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class AddUserCommand : IRequest<ApplicationUser>
    {
        public ApplicationUser User { get; set; }

        public AddUserCommand(ApplicationUser user)
        {
            User = user;
        }
    }
}
