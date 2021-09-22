using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.Mailing;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FluentValidation.TestHelper;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators;

namespace LibraryAccounting.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMessageSending _emailService;
        private readonly IMediator _mediator;
        public UserRegistrationCommand Register { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,
            IMessageSending emailService,
            IMediator mediator)
        {
            Register = new UserRegistrationCommand();
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
            _mediator = mediator;
        }

        public void OnGet()
        {
            _logger.LogInformation($"Register page visited");
        }

        public async Task<IActionResult> OnPost(UserRegistrationCommand register)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(register.Email);
                if (existingUser != null)
                {
                    if (await _userManager.IsEmailConfirmedAsync(existingUser) == false)
                    {
                        ModelState.AddModelError("", "Аккаунт с текущей почтой уже существует. Почта ещё не подтверждена. " +
                            "Чтобы подтвердить email перейдите по ссылке в письме");
                        _logger.LogInformation($"This account already exists, mail is not verified");
                        await this.SendMessage(existingUser, _userManager, _emailService);
                    }
                    else
                    {
                        _logger.LogInformation($"This account already exists");
                        ModelState.AddModelError("", "Аккаунт с текущей почтой уже существует");
                    }
                }
                else
                {
                    _logger.LogInformation($"Succeeded register");
                    await Registration(register);
                }
            }
            return Page();
        }

        private async Task Registration(UserRegistrationCommand register)
        {
            register.Page = this;
            var message = await _mediator.Send(register);
            ModelState.AddModelError(string.Empty, message);
        }
    }
}
