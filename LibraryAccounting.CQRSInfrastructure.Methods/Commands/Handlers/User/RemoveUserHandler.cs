using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers.User
{
    public class RemoveUserHandler : UserHandler, IRequestHandler<RemoveUserCommand, ApplicationUser>
    {
        public RemoveUserHandler(UserManager<ApplicationUser> db) : base(db)
        { }

        public async Task<ApplicationUser> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            await _db.DeleteAsync(user);
            return user;
        }
    }
}
