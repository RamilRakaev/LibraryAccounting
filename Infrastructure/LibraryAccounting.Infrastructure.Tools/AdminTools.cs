using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Tools
{
    public class AdminTools : IAdminTools
    {
        readonly private UserManager<ApplicationUser> _userRepository;
        readonly private IStorageRequests<ApplicationUserRole> _roleRepository;

        public AdminTools(UserManager<ApplicationUser> userRepository, IStorageRequests<ApplicationUserRole> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        #region add and remove
        public void AddUser(ApplicationUser user)
        {
            _userRepository.CreateAsync(user);
        }

        public void RemoveUser(ApplicationUser user)
        {
            _userRepository.DeleteAsync(user);
        }
        #endregion

        #region users
        public ApplicationUser GetUser(int id)
        {
            return _userRepository.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUser(IReturningResultHandler<ApplicationUser, ApplicationUser> resultHandler)
        {
            return resultHandler.Handle(_userRepository.Users.ToList());
        }

        public IEnumerable<ApplicationUser> GetUsers(IRequestsHandlerComponent<ApplicationUser> handlerComponent)
        {
            var elements = _userRepository.Users.ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.Users;
        }

        public IEnumerable<ApplicationUserRole> GetRoles()
        {
            return _roleRepository.GetAll();
        }
        #endregion
    }
}
