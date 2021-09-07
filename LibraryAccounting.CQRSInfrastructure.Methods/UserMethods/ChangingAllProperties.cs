using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
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

    public class ChangingAllUserPropertiesHandler : UserHandler, IRequestHandler<ChangingAllPropertiesCommand, User>
    {
        public ChangingAllUserPropertiesHandler(IRepository<User> _db) : base(_db)
        { }

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
            RuleFor(c => c.Name).NotEmpty().Length(3, 20);
            RuleFor(c => c.RoleId).NotEmpty();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(10);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
        }
    }
}
