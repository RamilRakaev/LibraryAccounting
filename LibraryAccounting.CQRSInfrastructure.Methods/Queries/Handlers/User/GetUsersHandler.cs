using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<ApplicationUser>>
    {
        private readonly UserManager<ApplicationUser> _db;

        public GetUsersHandler(UserManager<ApplicationUser> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(UserManager<ApplicationUser>));
        }

        public async Task<List<ApplicationUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _db.Users.Include(u => u.Role).AsNoTracking();
            QueryFilter<ApplicationUser, GetUsersQuery> filter =
                new QueryFilter<ApplicationUser, GetUsersQuery>(users);
            await Task.Run(() => filter.Filter(request));
            return users.ToList();
        }
    }
}
