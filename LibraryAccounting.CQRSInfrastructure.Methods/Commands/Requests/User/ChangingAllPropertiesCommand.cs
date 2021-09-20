using LibraryAccounting.Domain.Model;
using MediatR;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class ChangingAllPropertiesCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
