using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryAccounting.CQRSInfrastructure.Methods.UserMethods;
using System.Threading;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserFormModel : PageModel
    {
        readonly private IAdminTools AdminTools;
        public User UserInfo { get; set; }
        public SelectList Roles { get; set; }
        private readonly IMediator Mediator;

        public UserFormModel(IAdminTools adminTools, IMediator mediator)
        {
            AdminTools = adminTools ?? throw new ArgumentNullException(nameof(adminTools));
            Roles = new SelectList(AdminTools.GetRoles(), "Id", "Name");
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

        public async Task<IActionResult> OnPost(ChangingAllPropertiesCommand userInfo,
           CancellationToken token)
        {
            
            if (ModelState.IsValid)
            {
                await Mediator.Send(new ChangePasswordCommand() {Id = 4, Password = "password1" }, token);
                await Mediator.Send(userInfo, token);
                return RedirectToPage("/AdminPages/UserList");
            }
            return RedirectToPage("/AdminPages/UserForm");
        }
    }
}
