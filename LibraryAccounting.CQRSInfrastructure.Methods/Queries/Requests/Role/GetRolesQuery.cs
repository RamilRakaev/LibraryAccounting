using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests
{
    public class GetRolesQuery : IRequest<IEnumerable<ApplicationUserRole>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
