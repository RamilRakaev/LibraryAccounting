using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<ApplicationUser>>
    {
        private List<ApplicationUser> users;

        private readonly UserManager<ApplicationUser> _db;

        public GetUsersHandler(UserManager<ApplicationUser> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(UserManager<ApplicationUser>));
        }

        public async Task<List<ApplicationUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            users = _db.Users.ToList();
            QueryFilter<ApplicationUser, GetUsersQuery> filter =
                new QueryFilter<ApplicationUser, GetUsersQuery>(users);
            await Task.Run(() => filter.Filter(request));
            return users;
        }
    }
}
