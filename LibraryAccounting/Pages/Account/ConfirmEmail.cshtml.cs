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

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager,
            ILogger<ConfirmEmailModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(string userEmail, string code)
        {
            _logger.LogInformation($"ConfirmEmail page visited: {DateTime.Now:T}");
            if (userEmail == null || code == null)
            {
                _logger.LogError($"Arguments userEmail and code are zero: {DateTime.Now:T}");
                ModelState.AddModelError("", "Arguments are zero");
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                _logger.LogError($"User is not found: {DateTime.Now:T}");
                ModelState.AddModelError("", "User is not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("roleId", user.RoleId.ToString()));
                await _userManager.UpdateAsync(user);
                _logger.LogInformation($"User confirmation was successful: {DateTime.Now:T}");
                return RedirectToPage("/ClientPages/BookCatalog");
            }
            return Page();
        }
    }
}
