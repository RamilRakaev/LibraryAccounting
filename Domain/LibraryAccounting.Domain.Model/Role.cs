using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.Model
{
    public class Role : IdentityRole<int>
    {
        public List<User> Users { get; set; }
    }
}
