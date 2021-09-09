using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Tools
{
    public class AdminTools : IAdminTools
    {
        readonly private IRepository<ApplicationUser> UserRepository;
        readonly private IStorageRequests<ApplictionUserRole> RoleRepository;
        public AdminTools(IRepository<ApplicationUser> userRepository, IStorageRequests<ApplictionUserRole> roleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        #region add and remove
        public void AddUser(ApplicationUser user)
        {
            UserRepository.Add(user);
            UserRepository.Save();
        }

        public void RemoveUser(ApplicationUser user)
        {
            UserRepository.Remove(user);
            UserRepository.Save();
        }
        #endregion

        #region users
        public void EditUser(IVisitor<ApplicationUser> visitor, int id)
        {
            var user = UserRepository.Find(id);
            if(user != null)
            {
                if (!user.Accept(visitor))
                    throw new Exception("error when editing");
            }
        }

        public ApplicationUser GetUser(int id)
        {
            return UserRepository.Find(id);
        }

        public ApplicationUser GetUser(IReturningResultHandler<ApplicationUser, ApplicationUser> resultHandler)
        {
            return resultHandler.Handle(UserRepository.GetAll().ToList());
        }

        public IEnumerable<ApplicationUser> GetUsers(IRequestsHandlerComponent<ApplicationUser> handlerComponent)
        {
            var elements = UserRepository.GetAll().ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return UserRepository.GetAll();
        }

        public IEnumerable<ApplictionUserRole> GetRoles()
        {
            return RoleRepository.GetAll();
        }
        #endregion
    }
}
