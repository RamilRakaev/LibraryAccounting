using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class UserHandler
    {
        protected readonly UserManager<ApplicationUser> _db;

        public UserHandler(UserManager<ApplicationUser> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(UserManager<ApplicationUser>));
        }
    }
}
