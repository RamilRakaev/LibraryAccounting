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
        private readonly IMediator Mediator;
        public User UserInfo { get; set; }
        public SelectList Roles { get; set; }

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

        public async Task<IActionResult> OnPost(User userinfo, CancellationToken token)
        {

            if (ModelState.IsValid)
            {
                if (userinfo.Id == 0)
                    await Mediator.Send(new AddUserCommand(userinfo), token);
                else
                    await Mediator.Send(new ChangingAllPropertiesCommand()
                    {
                        Id = userinfo.Id,
                        Name = userinfo.Name,
                        Email = userinfo.Email,
                        Password = userinfo.Password,
                        RoleId = userinfo.RoleId
                    },
                        token);

                return RedirectToPage("/AdminPages/UserList");
            }
            return RedirectToPage("/AdminPages/UserForm");
        }
    }
}
