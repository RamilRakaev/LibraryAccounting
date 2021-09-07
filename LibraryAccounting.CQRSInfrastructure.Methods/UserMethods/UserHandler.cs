using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class UserHandler
    {
        protected readonly IRepository<User> db;

        public UserHandler(IRepository<User> _db)
        {
            db = _db ?? throw new ArgumentNullException(nameof(IRepository<User>));
        }
    }
}
