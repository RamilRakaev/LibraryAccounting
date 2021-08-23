using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Domain.ToolsInterfaces
{
    public interface IAdminTools
    {
        void AddUser(User user);

        void RemoveUser(User user);

        void SetPassword(User user, string Password);
    }
}
