using LibraryAccounting.CQRSInfrastructure.Mailing;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.Mailing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.Account
{
    public static class Notification
    {
        public static async Task SendMessage(this PageModel page, 
            ApplicationUser user, 
            UserManager<ApplicationUser> userManager,
            IOptions<EmailOptions> options)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = page.Url.Page(pageName: "/Account/ConfirmEmail", pageHandler: "", values: new { userEmail = user.Email, code },
                protocol: page.HttpContext.Request.Scheme);
            MessageSending emailService = new(options);
            await emailService.SendEmailAsync(user.Email, "Confirm your account",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
        }
    }
}
