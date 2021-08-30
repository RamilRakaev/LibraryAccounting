using LibraryAccounting.Domain.Interfaces.DataManagement;

namespace LibraryAccounting.Domain.Model
{
    public class User : IElement<User>
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User()
        {
        }

        public User(string name, string email, string password, Role role)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            RoleId = role.Id;
        }

        public User(string name, string email, string password, int roleId)
        {
            Name = name;
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
