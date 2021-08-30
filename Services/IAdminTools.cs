using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace LibraryAccounting.Services.ToolInterfaces
{
    public interface IAdminTools
    {
        #region add and remove
        void AddUser(User user);
        void RemoveUser(User user);
        #endregion

        #region get users
        void EditUser(IVisitor<User> visitor, int id);
        public User GetUser(int id);
        User GetUser(IReturningResultHandler<User, User> resultHandler);
        IEnumerable<User> GetUsers(IRequestsHandlerComponent<User> handlerComponent);
        IEnumerable<User> GetAllUsers();
        IEnumerable<Role> GetRoles();
        #endregion
    }
}
