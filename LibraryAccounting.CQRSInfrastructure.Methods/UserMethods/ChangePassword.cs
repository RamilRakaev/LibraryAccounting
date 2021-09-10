using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class ChangePasswordCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordHandler : UserHandler, IRequestHandler<ChangePasswordCommand, ApplicationUser>
    {
        public ChangePasswordHandler(UserManager<ApplicationUser> _db):base(_db)
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

        public class ChangePasswordValidator : AbstractValidator<ApplicationUser>
        {
            public ChangePasswordValidator()
            {
                RuleFor(c => c.Id).NotEmpty();
                RuleFor(c => c.Password).NotEmpty().MinimumLength(10);
            }
        }
    }
}
