using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.Mailing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMessageSending _emailService;
        public RegisterViewModel Register { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, 
            ILogger<RegisterModel> logger,
            IMessageSending emailService)
        {
            Register = new RegisterViewModel();
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
        }

        public void OnGet()
        {
            _logger.LogInformation($"Register page visited");
        }

        public async Task<IActionResult> OnPost(RegisterViewModel register)
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

        private async Task Registration(RegisterViewModel register)
        {
            ApplicationUser user = new() { UserName = register.Name, Email = register.Email, Password = register.Password, RoleId = 1 };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Sending message for user");
                await this.SendMessage(user, _userManager, _emailService);
                ModelState.AddModelError("", "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"{error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
    }
}
