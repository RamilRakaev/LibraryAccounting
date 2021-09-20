using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Queries.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _db;

        public GetUserHandler(UserManager<ApplicationUser> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(UserManager<ApplicationUser>));
        }

        public async Task<ApplicationUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
        }
    }
}
