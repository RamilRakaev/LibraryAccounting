using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryAccounting.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public ApplicationUser Login { get; set; }
        public string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Login = new ApplicationUser() { Id = 0 };
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Account/Register");
            return RedirectToPage("/Account/Login");
        }

        public IActionResult OnPost(ApplicationUser login)
        {
            if (ModelState.IsValid)
            {
                //var user = _adminTools.GetUser(new UserLoginHandlerAsync(login.Email, login.Password));
                //if (user != null)
                //{
                //    Authenticate(user);
                //    if(User.Identity.IsAuthenticated)
                //    switch (HttpContext.User.Claims.ElementAt(1).Value)
                //    {
                //        case "client":
                //            return RedirectToPage("/ClientPages/BookCatalog");
                //        case "librarian":
                //            return RedirectToPage("/LibrarianPages/BookCatalog");
                //        case "admin":
                //            return RedirectToPage("/AdminPages/UserList");
                //    }
                //    ModelState.AddModelError("", "there is no such role");
                //}
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }

        public async Task Authenticate(ApplicationUser user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
            new Claim("id", user.Id.ToString())
            };
            ClaimsIdentity id = new(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            ClaimsPrincipal principal = new ClaimsPrincipal(id);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
