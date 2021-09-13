﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Domain.Model
{
    public class ApplictionUserRole : IdentityRole<int>
    {
        public List<ApplicationUser> Users { get; set; }
    }
}
