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
using LibraryAccounting.CQRSInfrastructure.Methods.RoleMethods;

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
            EstablishRoles();
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void EstablishRoles()
        {
            var command = new GetRolesCommand();
            var roles = Mediator.Send(command, new CancellationToken(false));
            Thread.Sleep(1000);
            Roles = new SelectList(roles.Result, "Id", "Name");
        }

        public async void OnGet(int? id)
        {
            if (id != null)
            {
                UserInfo = await Mediator.Send(new GetUserQuery() { Id = Convert.ToInt32(id) }, new CancellationToken(false));
            }
            else
            {
                UserInfo = new User();
            }
        }

        public async Task<IActionResult> OnPost(AddUserCommand userInfo, CancellationToken token)
        {

            if (ModelState.IsValid)
            {
                await Mediator.Send(userInfo, token);
                return RedirectToPage("/AdminPages/UserList");
            }
            return RedirectToPage("/AdminPages/UserForm");
        }
    }
}
