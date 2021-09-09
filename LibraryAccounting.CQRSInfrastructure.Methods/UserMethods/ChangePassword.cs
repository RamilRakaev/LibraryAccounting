using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
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
        public ChangePasswordHandler(IRepository<ApplicationUser> _db):base(_db)
        { }

        public async Task<ApplicationUser> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _db.Find(request.Id);
            if (user != null)
            {
                user.Password = request.Password;
                await _db.SaveAsync();
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
