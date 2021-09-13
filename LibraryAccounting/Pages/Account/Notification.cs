using LibraryAccounting.CQRSInfrastructure.EmailManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.Account
{
    public static class Notification
    {
        public static async Task SendMessage(this PageModel page, ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = page.Url.Page(pageName: "/Account/ConfirmEmail", pageHandler: "", values: new { userEmail = user.Email, code = code },
                protocol: page.HttpContext.Request.Scheme);
            MessageSending emailService = new MessageSending();
            await emailService.SendEmailAsync(user.Email, "Confirm your account",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
        }
    }
}
