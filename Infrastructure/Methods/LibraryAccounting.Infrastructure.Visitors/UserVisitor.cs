using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Visitors
{
    public class ChangePasswordVisitor : IVisitor<User>
    {
        readonly private string Password;
        public ChangePasswordVisitor(string password)
        {
            Password = password;
        }
        public bool Visit(User element)
        {
            element.Password = Password;
            return true;
        }
    }
}
