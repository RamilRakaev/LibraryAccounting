using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Pages.ClientPages;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly ILogger<ConfirmEmailModel> _logger;
        private readonly IMediator _mediator;

        public ConfirmEmailModel(
            ILogger<ConfirmEmailModel> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> OnGet(string userEmail, string code)
        {
            _logger.LogInformation($"ConfirmEmail page visited");
            var user = await _mediator.Send(new ConfirmEmailCommand(userEmail, code));
            switch (user.RoleId)
            {
                case 1:
                    return RedirectToPage("/ClientPages/BookCatalog");
                case 2:
                    return RedirectToPage("/LibrarianPages/BookCatalog");
                case 3:
                    return RedirectToPage("/AdminPages/UserList");
            }
            return RedirectToPage("/Account/Login");
        }
    }
}
