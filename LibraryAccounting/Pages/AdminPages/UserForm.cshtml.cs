using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserFormModel : PageModel
    {
        readonly private IAdminTools AdminTools;
        public User UserInfo { get; set; }
        public SelectList Roles { get; set; }
        public UserFormModel(IAdminTools adminTools)
        {
            AdminTools = adminTools;
            Roles = new SelectList(AdminTools.GetRoles(), "Id", "Name");
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                UserInfo = AdminTools.GetUser(Convert.ToInt32(id));
            }
            else
            {
                UserInfo = new User();
            }
        }

        public IActionResult OnPost(User userInfo)
        {
            if (ModelState.IsValid)
            {
                AdminTools.RemoveUser(userInfo);
                userInfo.Id = 0;
                AdminTools.AddUser(userInfo);
                return RedirectToPage("/AdminPages/UserList");
            }
            return RedirectToPage("/AdminPages/UserForm");
        }
    }
}
