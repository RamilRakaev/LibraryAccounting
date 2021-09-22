using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public ApplicationUser User { get; set; }

        public CreateUserCommand(ApplicationUser user)
        {
            User = user;
        }
    }
}
