using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Visitors
{
    public class ChangePasswordVisitor : IVisitor<ApplicationUser>
    {
        readonly private string _password;

        public ChangePasswordVisitor(string password)
        {
            _password = password;
        }

        public bool Visit(ApplicationUser element)
        {
            element.Password = _password;
            return true;
        }
    }
}
