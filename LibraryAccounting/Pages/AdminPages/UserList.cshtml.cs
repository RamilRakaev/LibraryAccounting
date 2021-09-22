using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Pages.ClientPages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserListModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly UserProperties _user;
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public UserListModel(IMediator mediator,
            UserProperties userProperties)
        {
            _mediator = mediator;
            _user = userProperties;
        }

        public async Task<IActionResult> OnGet()
        {
            if (_user.IsAuthenticated == false || _user.RoleId != 2)
            {
                return RedirectToPage("/Account/Login");
            }
            Users = await _mediator.Send(new GetUsersQuery(), new CancellationToken(false));
            return Page();
        }

        public async Task OnPost(int id, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new RemoveUserCommand() { Id = id }, token);
            }
            Users = await _mediator.Send(new GetUsersQuery(), token);
        }
    }
}
