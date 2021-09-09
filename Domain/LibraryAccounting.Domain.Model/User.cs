using LibraryAccounting.Domain.Interfaces.DataManagement;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Domain.Model
{
    public class User : IdentityUser<int>
    {
        public int RoleId { get; set; }
        public UserRole Role { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string name, string email, string password, UserRole role)
        {
            UserName = name;
            Email = email;
            Password = password;
            Role = role;
            RoleId = role.Id;
        }

        public User(string name, string email, string password, int roleId)
        {
            UserName = name;
            Email = email;
            Password = password;
            RoleId = roleId;
        }

        public bool Accept(IVisitor<User> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
