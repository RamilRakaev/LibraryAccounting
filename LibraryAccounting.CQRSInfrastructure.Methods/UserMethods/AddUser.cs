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
    public class AddUserCommand : IRequest<User>
    {

        public User User { get; set; }

        public AddUserCommand(User user)
        {
            User = user;
        }
    }

    public class AddUserHandler : UserHandler, IRequestHandler<AddUserCommand, User>
    {
        public AddUserHandler(IRepository<User> db) : base(db)
        { }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _db.AddAsync(request.User);
            await _db.SaveAsync();
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
