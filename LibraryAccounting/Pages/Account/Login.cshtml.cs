using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.Pages.Account
{
    public class LoginModel : AuthenticateUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LoginViewModel Login { get; set; }

        public LoginModel(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationUserRole> roleManager)
        {
            Login = new LoginViewModel() { ReturnUrl = null };
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPost(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    login.Email,
                    login.Password,
                    login.RememberMe,
                    false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl))
                    {
                        return RedirectToPage(login.ReturnUrl);
                    }
                    else
                    {
                        var user = await _userManager.FindByEmailAsync(login.Email);
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
                }
                else
                {
                    ModelState.AddModelError("", "������������ ����� � (���) ������");
                }
            }
            return Page();
        }
    }
}
