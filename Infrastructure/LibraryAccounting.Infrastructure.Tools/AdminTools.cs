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
        readonly private IRepository<User> UserRepository;
        readonly private IStorageRequests<UserRole> RoleRepository;
        public AdminTools(IRepository<User> userRepository, IStorageRequests<UserRole> roleRepository)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        #region add and remove
        public void AddUser(User user)
        {
            UserRepository.Add(user);
            UserRepository.Save();
        }

        public void RemoveUser(User user)
        {
            UserRepository.Remove(user);
            UserRepository.Save();
        }
        #endregion

        #region users
        public void EditUser(IVisitor<User> visitor, int id)
        {
            var user = UserRepository.Find(id);
            if(user != null)
            {
                if (!user.Accept(visitor))
                    throw new Exception("error when editing");
            }
        }

        public User GetUser(int id)
        {
            return UserRepository.Find(id);
        }

        public User GetUser(IReturningResultHandler<User, User> resultHandler)
        {
            return resultHandler.Handle(UserRepository.GetAll().ToList());
        }

        public IEnumerable<User> GetUsers(IRequestsHandlerComponent<User> handlerComponent)
        {
            var elements = UserRepository.GetAll().ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UserRepository.GetAll();
        }

        public IEnumerable<UserRole> GetRoles()
        {
            return RoleRepository.GetAll();
        }
        #endregion
    }
}
