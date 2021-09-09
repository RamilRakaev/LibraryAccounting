using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class UserHandler
    {
        protected readonly IRepository<ApplicationUser> _db;

        public UserHandler(IRepository<ApplicationUser> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(IRepository<ApplicationUser>));
        }
    }
}
