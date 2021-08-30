using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class RoleRequests : IStorageRequests<Role>
    {
        readonly private DataContext db;
        public RoleRequests(DataContext context)
        {
            db = context;
        }

        public Role Find(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }
    }
}
