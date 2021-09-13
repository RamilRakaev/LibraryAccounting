using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryAccounting.Pages.ClientPages
{
    public class UserProperties
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserProperties(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public int UserId
        {
            get
            {
                return Convert.ToInt32(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        public int RoleId
        {
            get
            {
                return Convert.ToInt32(_httpContext.HttpContext.User.FindFirst("roleId").Value);
            }
        }

        public bool IsAuthenticated
        {
            get { return _httpContext.HttpContext.User.Identity.IsAuthenticated; }
        }

    }
}
