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
        readonly private IRepository<ApplicationUser> _userRepository;
        readonly private IStorageRequests<ApplicationUserRole> _roleRepository;

        public AdminTools(IRepository<ApplicationUser> userRepository, IStorageRequests<ApplicationUserRole> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        #region add and remove
        public void AddUser(ApplicationUser user)
        {
            _userRepository.Add(user);
            _userRepository.Save();
        }

        public void RemoveUser(ApplicationUser user)
        {
            _userRepository.Remove(user);
            _userRepository.Save();
        }
        #endregion

        #region users
        public void EditUser(IVisitor<ApplicationUser> visitor, int id)
        {
            var user = _userRepository.Find(id);
            if(user != null)
            {
                if (!user.Accept(visitor))
                    throw new Exception("error when editing");
            }
        }

        public ApplicationUser GetUser(int id)
        {
            return _userRepository.Find(id);
        }

        public ApplicationUser GetUser(IReturningResultHandler<ApplicationUser, ApplicationUser> resultHandler)
        {
            return resultHandler.Handle(_userRepository.GetAll().ToList());
        }

        public IEnumerable<ApplicationUser> GetUsers(IRequestsHandlerComponent<ApplicationUser> handlerComponent)
        {
            var elements = _userRepository.GetAll().ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<ApplicationUserRole> GetRoles()
        {
            return _roleRepository.GetAll();
        }
        #endregion
    }
}
