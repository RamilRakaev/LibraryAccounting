using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryAccounting.Domain.Model
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int RoleId { get; set; }
        public ApplicationUserRole Role { get; set; }
        public List<Booking> Bookings { get; set; }
        public string Password { get; set; }

        public ApplicationUser()
        { }

        public ApplicationUser(string name, string email, string password, ApplicationUserRole role)
        {
            UserName = name;
            Email = email;
            Password = password;
            RoleId = role.Id;
            Role = role;
        }

        public ApplicationUser(string name, string email, string password, int roleId)
        {
            UserName = name;
            Email = email;
            Password = password;
            RoleId = roleId;
        }
    }
}
