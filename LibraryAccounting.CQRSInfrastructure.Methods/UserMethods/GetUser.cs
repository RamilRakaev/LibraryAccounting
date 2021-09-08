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
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class GetUserHandler : UserHandler, IRequestHandler<GetUserQuery, User>
    {
        public GetUserHandler(IRepository<User> _db) : base(_db)
        { }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _db.FindAsync(request.Id);
        }
    }
}
