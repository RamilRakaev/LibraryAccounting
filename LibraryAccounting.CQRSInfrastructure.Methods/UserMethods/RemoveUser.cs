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
    public class RemoveUserCommand : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class RemoveUserHandler : UserHandler, IRequestHandler<RemoveUserCommand, User>
    {
        public RemoveUserHandler(IRepository<User> _db) : base(_db)
        { }

        public async Task<User> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
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
}
