using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class RemoveUserCommand : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
    }

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

    public class RemoveUserValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserValidator()
        {
            RuleFor(b => b.Id).NotEqual(0);
        }
    }
}
