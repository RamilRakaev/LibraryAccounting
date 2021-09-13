using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterViewModel Register { get; set; }
        public RegisterModel(UserManager<ApplicationUser> userManager)
        {
            Register = new RegisterViewModel();
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(register.Email);
                if (existingUser != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(existingUser))
                    {
                        ModelState.AddModelError("", "������� � ������� ������ ��� ����������. ����� ��� �� ������������. " +
                            "����� ����������� email ��������� �� ������ � ������");
                        await this.SendMessage(existingUser, _userManager);
                    }
                    else
                    {
                        ModelState.AddModelError("", "������� � ������� ������ ��� ����������");
                    }
                }
                else
                {
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

                await this.SendMessage(user, _userManager);
                ModelState.AddModelError("", "��� ���������� ����������� ��������� ����������� ����� � ��������� �� ������, ��������� � ������");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

    }
}
