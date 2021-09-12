
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Antlr.Runtime;
using LibraryAccounting.CQRSInfrastructure.EmailManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationUserRole> _roleManager;
        public RegisterViewModel Register { get; set; }
        public RegisterModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationUserRole> roleManager)
        {
            Register = new RegisterViewModel();
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(RegisterViewModel register)
        {
            //if (ModelState.IsValid)
            //{
            //    ApplicationUser user = new() { UserName = register.Name, Email = register.Email, RoleId = 1 };
            //    var result = await _userManager.CreateAsync(user, register.Password);
            //    if (result.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(user, false);
            //        await _userManager.AddClaimAsync(user, new Claim("roleId", user.RoleId.ToString()));
            //        await _userManager.UpdateAsync(user);
            //        await _userManager.ConfirmEmailAsync(user);
            //        return RedirectToPage("/ClientPages/BookCatalog");
            //    }
            //    else
            //    {
            //        foreach (var error in result.Errors)
            //        {
            //            ModelState.AddModelError(string.Empty, error.Description);
            //        }
            //    }
            //}
            if (ModelState.IsValid)
            {
                ApplicationUser user = new() { UserName = register.Name, Email = register.Email, Password = register.Password, RoleId = 1};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    // генерация токена для пользователя
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Register",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    MessageSending emailService = new MessageSending();
                    await emailService.SendEmailAsync(register.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Page();
        }
    }
}
