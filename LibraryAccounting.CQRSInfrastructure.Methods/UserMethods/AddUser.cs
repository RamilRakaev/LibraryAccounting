using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class AddUserCommand : IRequest<ApplicationUser>
    {

        public ApplicationUser User { get; set; }

        public AddUserCommand(ApplicationUser user)
        {
            User = user;
        }
    }

    public class AddUserHandler : UserHandler, IRequestHandler<AddUserCommand, ApplicationUser>
    {
        public AddUserHandler(UserManager<ApplicationUser> db) : base(db)
        { }

        public async Task<ApplicationUser> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _db.CreateAsync(request.User);
            return request.User;
        }

        public class AddUserValidator : AbstractValidator<AddUserCommand>
        {
            public AddUserValidator()
            {
                RuleFor(c => c.User.UserName).NotEmpty().Length(3, 20);
                RuleFor(u => u.User.Email).NotEmpty().EmailAddress();
                RuleFor(u => u.User.Password).NotEmpty().MinimumLength(10);
                RuleFor(u => u.User.RoleId).NotEmpty();
            }
        }
    }
}
