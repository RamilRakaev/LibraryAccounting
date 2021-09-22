using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LibraryAccounting.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager,
            ILogger<ConfirmEmailModel> logger,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(string userEmail, string code)
        {
            _logger.LogInformation($"ConfirmEmail page visited");
            if (userEmail == null || code == null)
            {
                _logger.LogError($"Arguments userEmail and code are zero");
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                _logger.LogError($"User is not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User confirmation was successful");
                if (_signInManager.PasswordSignInAsync(user.Email, user.Password, false, false).Result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddClaimAsync(user, new Claim("roleId", user.RoleId.ToString()));
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation($"Succeeded login");
                    switch (user.RoleId)
                    {
                        case 1:
                            return RedirectToPage("/ClientPages/BookCatalog");
                        case 2:
                            return RedirectToPage("/LibrarianPages/BookCatalog");
                        case 3:
                            return RedirectToPage("/AdminPages/UserList");
                    }
                }
                return RedirectToPage("/ClientPages/BookCatalog");
            }
            return RedirectToPage("/Account/Login");
        }
    }
}
