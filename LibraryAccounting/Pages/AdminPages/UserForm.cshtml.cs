using System;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
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
        private readonly IMediator _mediator;
        public ApplicationUser UserInfo { get; set; }
        public SelectList Roles { get; set; }

        public UserFormModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            EstablishRoles();
        }

        public void EstablishRoles()
        {
            var command = new GetRolesCommand();
            var roles = _mediator.Send(command, new CancellationToken(false)).Result;
            Roles = new SelectList(roles, "Id", "Name");
        }

        public async Task OnGet(int? id)
        {
            if (id != null)
            {
                UserInfo = await _mediator.Send(new GetUserQuery() { Id = Convert.ToInt32(id) }, new CancellationToken(false));
            }
            else
            {
                UserInfo = new ApplicationUser();
            }
        }

        public async Task<IActionResult> OnPost(ApplicationUser userinfo, CancellationToken token)
        {

            if (ModelState.IsValid)
            {
                if (userinfo.Id == 0)
                    await _mediator.Send(new AddUserCommand(userinfo), token);
                else
                    await _mediator.Send(new ChangingAllPropertiesCommand()
                    {
                        Id = userinfo.Id,
                        Name = userinfo.UserName,
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
