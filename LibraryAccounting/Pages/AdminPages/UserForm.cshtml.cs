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
        public User UserInfo { get; set; }
        public SelectList Roles { get; set; }
        private readonly IMediator Mediator;

        public UserFormModel(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            EstablishRoles();
        }

        public async void EstablishRoles()
        {
            var command = new GetRolesCommand();
            var roles = await Mediator.Send(command, new CancellationToken(false));
            Roles = new SelectList(roles, "Id", "Name");
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
