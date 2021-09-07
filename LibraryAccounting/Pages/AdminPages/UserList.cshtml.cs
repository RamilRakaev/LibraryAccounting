using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<User> Users { get; set; }
        private readonly IMediator Mediator;

        public UserListModel(IAdminTools adminTools, IMediator mediator)
        {
            AdminTools = adminTools;
            Mediator = mediator;
        }

        public async void OnGet()
        {
            Users = AdminTools.GetAllUsers().ToList();
            await Mediator.Send(new GetUsersCommand(), new System.Threading.CancellationToken(false));
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
