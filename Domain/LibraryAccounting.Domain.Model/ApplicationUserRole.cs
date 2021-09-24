using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.Model
{
    public class ApplicationUserRole : IdentityRole<int>
    {
        public List<ApplicationUser> Users { get; set; }
    }
}
