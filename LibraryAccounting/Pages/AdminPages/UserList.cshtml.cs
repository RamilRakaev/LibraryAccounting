using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.UserMethods;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserListModel : PageModel
    {
        private readonly IMediator _mediator;
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public UserListModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void OnGet()
        {
            Users = await _mediator.Send(new GetUsersQuery(), new CancellationToken(false));
        }

        public async Task OnPost(int id, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new RemoveUserCommand() { Id = id }, token);
            }
            await ExtractUsersAsync(token);
        }

        private async Task ExtractUsersAsync(CancellationToken token)
        {
            Users = await _mediator.Send(new GetUsersQuery(), token);
        }
    }
}
