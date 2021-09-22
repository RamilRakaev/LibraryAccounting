using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User
{
    public class UserLoginCommand : IRequest<IdentityResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
