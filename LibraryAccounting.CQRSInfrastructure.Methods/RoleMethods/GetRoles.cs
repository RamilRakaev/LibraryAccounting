﻿using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.RoleMethods
{
    public class GetRolesCommand : IRequest<IEnumerable<ApplicationUserRole>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }

    public class GetRolesHander : IRequestHandler<GetRolesCommand, IEnumerable<ApplicationUserRole>>
    {
        private readonly RoleManager<ApplicationUserRole> _db;

        public GetRolesHander(RoleManager<ApplicationUserRole> db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ApplicationUserRole>> Handle(GetRolesCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == 0)
                return await Task.Run(() => _db.Roles);
            else
            {
                return await Task.Run(() => _db.Roles.
                Where(r => r.Name == request.Name));
            }
        }
    }
}
