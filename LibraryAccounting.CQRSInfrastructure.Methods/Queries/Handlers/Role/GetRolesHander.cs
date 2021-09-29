using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers.Role
{
    public class GetRolesHander : IRequestHandler<GetRolesQuery, IEnumerable<ApplicationUserRole>>
    {
        private readonly RoleManager<ApplicationUserRole> _db;

        public GetRolesHander(RoleManager<ApplicationUserRole> db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ApplicationUserRole>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == 0)
                return await Task.Run(() => _db.Roles);
            else
            {
                return await Task.Run(() => _db.Roles.
                Where(r => r.Name == request.Name));
            }
        }
    }
}
