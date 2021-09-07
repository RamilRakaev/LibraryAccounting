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
    public class ChangePasswordCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordHandler : UserHandler, IRequestHandler<ChangePasswordCommand, User>
    {
        public ChangePasswordHandler(IRepository<User> _db):base(_db)
        { }

        public async Task<User> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = db.Find(request.Id);
            if (user != null)
            {
                user.Password = request.Password;
                await db.SaveAsync();
                return user;
            }
            else
            {
                throw new Exception();
            }
        }

        public class ChangePasswordValidator : AbstractValidator<User>
        {
            public ChangePasswordValidator()
            {
                RuleFor(c => c.Id).NotEmpty();
                RuleFor(c => c.Password).NotEmpty().MinimumLength(10);
            }
        }
    }
}
