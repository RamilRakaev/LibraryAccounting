using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
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
        public RemoveUserHandler(IRepository<ApplicationUser> _db) : base(_db)
        { }

        public async Task<ApplicationUser> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = _db.Find(request.Id);
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            _db.Remove(user);
            await _db.SaveAsync();
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
