using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class ChangePasswordHandler : UserHandler, IRequestHandler<ChangePasswordCommand, ApplicationUser>
    {
        public ChangePasswordHandler(UserManager<ApplicationUser> db) : base(db)
        { }

        public async Task<ApplicationUser> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (user != null)
            {
                user.Password = request.Password;
                return user;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
