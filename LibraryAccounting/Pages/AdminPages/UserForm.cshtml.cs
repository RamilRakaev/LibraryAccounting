using System;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using Microsoft.Extensions.Logging;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.Pages.AdminPages
{
    public class UserFormModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserFormModel> _logger;
        public ApplicationUser UserInfo { get; set; }
        public SelectList Roles { get; set; }

        public UserFormModel(IMediator mediator,
            ILogger<UserFormModel> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
            ExtractRoles();
        }

        public void ExtractRoles()
        {
            var command = new GetRolesQuery();
            var roles = _mediator.Send(command, new CancellationToken(false)).Result;
            Roles = new SelectList(roles, "Id", "Name");
            _logger.LogDebug($"Roles extracted");
        }

        public async Task OnGet(int? id)
        {
            _logger.LogInformation($"UserForm page visited:");
            if (id != null)
            {
                _logger.LogDebug($"id is not zero");
                UserInfo = await _mediator.Send(new GetUserQuery() { Id = Convert.ToInt32(id) }, new CancellationToken(false));
            }
            else
            {
                _logger.LogDebug($"id is zero:");
                UserInfo = new ApplicationUser();
            }
        }

        public async Task<IActionResult> OnPost(ApplicationUser userinfo, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                if (userinfo.Id == 0)
                {
                    await _mediator.Send(new AddUserCommand(userinfo), token);
                    _logger.LogDebug($"Added user");
                }
                else
                {
                    await _mediator.Send(new ChangingAllPropertiesCommand()
                    {
                        Id = userinfo.Id,
                        Name = userinfo.UserName,
                        Email = userinfo.Email,
                        Password = userinfo.Password,
                        RoleId = userinfo.RoleId
                    },
                     token);
                    _logger.LogDebug($"Changing all properties for user");
                }
                _logger.LogInformation($"Modelstate is valid");
                return RedirectToPage("/AdminPages/UserList");
            }
            _logger.LogWarning($"Modelstate is not valid");
            return RedirectToPage("/AdminPages/UserForm");
        }
    }
}
