using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Domain.Model
{
    public class UserRole : IdentityRole<int>
    {
        public List<User> Users { get; set; }
    }
}
