using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserListModel : PageModel
    {
        readonly private IAdminTools AdminTools;
        public List<User> Users { get; set; }
        public UserListModel(IAdminTools adminTools)
        {
            AdminTools = adminTools;
        }

        public void OnGet()
        {
            Users = AdminTools.GetAllUsers().ToList();
        }

        public void OnPost(User user)
        {
            if (ModelState.IsValid)
            {
                AdminTools.RemoveUser(user);
            }
            Users = AdminTools.GetAllUsers().ToList();
        }
    }
}
