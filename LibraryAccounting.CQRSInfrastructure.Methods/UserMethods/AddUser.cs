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
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class AddUserHandler : UserHandler, IRequestHandler<AddUserCommand, User>
    {
        public AddUserHandler(IRepository<User> _db) : base(_db)
        { }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Name, request.Email, request.Password, request.RoleId);
            await db.AddAsync(user);
            await db.SaveAsync();
            return user;
        }

        public class AddUserValidator : AbstractValidator<AddUserCommand>
        {
            public AddUserValidator()
            {
                RuleFor(c => c.Name).NotEmpty().Length(3, 20);
                RuleFor(u => u.Email).NotEmpty().EmailAddress();
                RuleFor(u => u.Password).NotEmpty().MinimumLength(10);
                RuleFor(u => u.RoleId).NotEmpty();
            }
        }
    }
}
