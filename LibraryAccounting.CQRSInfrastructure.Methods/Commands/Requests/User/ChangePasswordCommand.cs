using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class ChangePasswordCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
