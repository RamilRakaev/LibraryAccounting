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
    public class RemoveUserCommand :  IRequest<User>
    {
        public int Id { get; set; }
    }

    public class RemoveUserHandler : UserHandler, IRequestHandler<RemoveUserCommand, User>
    {
        public RemoveUserHandler(IRepository<User> _db): base(_db)
        { }

        public async Task<User> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await db.FindAsync(request.Id);
            db.Remove(user);
            if(user != null) {
                throw new NullReferenceException();
            }
            return user;
        }
    }
}
