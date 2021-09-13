using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string userEmail, string code)
        {
            if (userEmail == null || code == null)
            {
                ModelState.AddModelError("", "Arguments are zero");
                return Page();
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "User is not found");
                return Page();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToPage("/ClientPages/BookCatalog");
            else
                return Page();
        }
    }
}
