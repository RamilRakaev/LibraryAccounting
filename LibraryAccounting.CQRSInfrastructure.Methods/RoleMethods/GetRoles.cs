using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.RoleMethods
{
    public class GetRolesCommand : IRequest<IEnumerable<UserRole>>
    {
        public int UserId { get; set; }
    }

    public class GetRolesHander : IRequestHandler<GetRolesCommand, IEnumerable<UserRole>>
    {
        private readonly IStorageRequests<UserRole> db;

        public GetRolesHander(IStorageRequests<UserRole> _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<UserRole>> Handle(GetRolesCommand request, CancellationToken cancellationToken)
        {
            if(request.UserId == 0)
            return await Task.Run(() => db.GetAll());
            else
            {
                return db.GetAll();
            }
        }
    }
}
