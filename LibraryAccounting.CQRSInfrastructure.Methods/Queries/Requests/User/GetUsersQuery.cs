using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetUsersQuery : IRequest<List<ApplicationUser>>
    {
        public string Name { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
