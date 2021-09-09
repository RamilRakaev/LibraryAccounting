using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace LibraryAccounting.Services.ToolInterfaces
{
    public interface IAdminTools
    {
        #region add and remove
        void AddUser(ApplicationUser user);
        void RemoveUser(ApplicationUser user);
        #endregion

        #region get users
        void EditUser(IVisitor<ApplicationUser> visitor, int id);
        public ApplicationUser GetUser(int id);
        ApplicationUser GetUser(IReturningResultHandler<ApplicationUser, ApplicationUser> resultHandler);
        IEnumerable<ApplicationUser> GetUsers(IRequestsHandlerComponent<ApplicationUser> handlerComponent);
        IEnumerable<ApplicationUser> GetAllUsers();
        IEnumerable<ApplictionUserRole> GetRoles();
        #endregion
    }
}
