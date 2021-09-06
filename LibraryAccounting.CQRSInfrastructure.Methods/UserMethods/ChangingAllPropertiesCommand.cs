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
    public class ChangingAllPropertiesCommand : IRequest<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class ChangingAllUserPropertiesHandler : IRequestHandler<ChangingAllPropertiesCommand, User>
    {
        private readonly IRepository<User> db;

        public ChangingAllUserPropertiesHandler(IRepository<User> _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(IRepository<User>));
        }

        public async Task<User> Handle(ChangingAllPropertiesCommand command, CancellationToken cancellationToken)
        {
            var user = db.Find(command.Id);
            user.Name = command.Name;
            user.Email = command.Email;
            user.Password = command.Password;
            user.RoleId = command.RoleId;
            await db.SaveAsync();

            return user;
        }
    }

    public class ChangingAllUserPropertiesValidator : AbstractValidator<ChangingAllPropertiesCommand>
    {
        public ChangingAllUserPropertiesValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.RoleId).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Email).NotEmpty();
        }
    }
}
