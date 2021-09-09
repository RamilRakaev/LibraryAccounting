using LibraryAccounting.Domain.Interfaces.DataManagement;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Domain.Model
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int RoleId { get; set; }
        public ApplictionUserRole Role { get; set; }
        public string Password { get; set; }

        public ApplicationUser()
        {
        }

        public ApplicationUser(string name, string email, string password, ApplictionUserRole role)
        {
            UserName = name;
            
            Email = email;
            Password = password;
            Role = role;
            RoleId = role.Id;
        }

        public ApplicationUser(string name, string email, string password, int roleId)
        {
            UserName = name;
            Email = email;
            Password = password;
            RoleId = roleId;
        }

        public bool Accept(IVisitor<ApplicationUser> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
