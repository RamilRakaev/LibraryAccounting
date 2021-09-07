using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.UserMethods;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserListModel : PageModel
    {
        readonly private IAdminTools AdminTools;
        public List<User> Users { get; set; } = new List<User>();
        private readonly IMediator Mediator;

        public UserListModel(IAdminTools adminTools, IMediator mediator)
        {
            AdminTools = adminTools;
            Mediator = mediator;
        }

        public async void OnGet()
        {
            Users = await Mediator.Send(new GetUsersQuery(), new CancellationToken(false));
        }

        public async void OnPost(int id, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new RemoveUserCommand() { Id = id }, token);
            }
            Task.WaitAll(new Task(() => ExtractUsers(token)));
        }

        private void ExtractUsers(CancellationToken token)
        {
            Users = Mediator.Send(new GetUsersQuery(), token).Result;
        }

    }
}
