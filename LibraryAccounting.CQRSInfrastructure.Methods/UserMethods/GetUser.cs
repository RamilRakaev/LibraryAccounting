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
    public class GetUserCommand : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class GetUserHandler : UserHandler, IRequestHandler<GetUserCommand, User>
    {
        public GetUserHandler(IRepository<User> _db) : base(_db)
        { }

        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            return await db.FindAsync(request.Id);
        }
    }
}
