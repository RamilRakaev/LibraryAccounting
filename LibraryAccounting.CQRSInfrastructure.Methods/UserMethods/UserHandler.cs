using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class UserHandler
    {
        protected readonly IRepository<User> _db;

        public UserHandler(IRepository<User> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(IRepository<User>));
        }
    }
}
