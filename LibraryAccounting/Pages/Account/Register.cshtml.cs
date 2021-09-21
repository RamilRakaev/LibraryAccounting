using System;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.Mailing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LibraryAccounting.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IOptions<EmailOptions> _options;
        public RegisterViewModel Register { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, 
            ILogger<RegisterModel> logger,
            IOptions<EmailOptions> options)
        {
            Register = new RegisterViewModel();
            _userManager = userManager;
            _logger = logger;
            _options = options;
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
                        ModelState.AddModelError("", "������� � ������� ������ ��� ����������. ����� ��� �� ������������. " +
                            "����� ����������� email ��������� �� ������ � ������");
                        _logger.LogInformation($"This account already exists, mail is not verified");
                        await this.SendMessage(existingUser, _userManager, _options);
                    }
                    else
                    {
                        _logger.LogInformation($"This account already exists");
                        ModelState.AddModelError("", "������� � ������� ������ ��� ����������");
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
                await this.SendMessage(user, _userManager, _options);
                ModelState.AddModelError("", "��� ���������� ����������� ��������� ����������� ����� � ��������� �� ������, ��������� � ������");
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
