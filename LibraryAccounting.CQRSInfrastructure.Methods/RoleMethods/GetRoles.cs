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
    public class GetRolesCommand : IRequest<IEnumerable<ApplictionUserRole>>
    {
        public int UserId { get; set; }
    }

    public class GetRolesHander : IRequestHandler<GetRolesCommand, IEnumerable<ApplictionUserRole>>
    {
        private readonly IStorageRequests<ApplictionUserRole> db;

        public GetRolesHander(IStorageRequests<ApplictionUserRole> _db)
        {
            db = _db;
        }

        public async Task<IEnumerable<ApplictionUserRole>> Handle(GetRolesCommand request, CancellationToken cancellationToken)
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
