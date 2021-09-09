using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class RoleRequests : IStorageRequests<UserRole>
    {
        readonly private DataContext db;
        public RoleRequests(DataContext context)
        {
            db = context;
        }

        public UserRole Find(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<UserRole> GetAll()
        {
            return db.Roles;
        }
    }
}
