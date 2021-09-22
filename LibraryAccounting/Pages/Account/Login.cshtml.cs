using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Pages.ClientPages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;
        private readonly UserProperties _userProperties;
        public UserLoginCommand Login { get; set; }

        public LoginModel(IMediator mediator,
            ILogger<LoginModel> logger,
            UserProperties userProperties)
        {
            Login = new UserLoginCommand();
            _mediator = mediator;
            _logger = logger;
            _userProperties = userProperties;
        }

        public IActionResult OnGet()
        {
            _logger.LogInformation($"Login page visited");
            if (_userProperties.IsAuthenticated)
                switch (_userProperties.RoleId)
                {
                    case 1:
                        return RedirectToPage("/ClientPages/BookCatalog");
                    case 2:
                        return RedirectToPage("/LibrarianPages/BookCatalog");
                    case 3:
                        return RedirectToPage("/AdminPages/UserList");
                }
            return Page();
        }

        public async Task<IActionResult> OnPost(UserLoginCommand login)
        {
            if (ModelState.IsValid)
            {
                login.Page = this;
                string message = await _mediator.Send(login);
                ModelState.AddModelError(string.Empty, message);

            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                _logger.LogWarning($"Incorrect username and (or) password");
            }
            return RedirectToPage("/Account/Login");
        }
    }
}
