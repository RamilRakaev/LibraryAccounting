﻿using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User
{
    public class UserLoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public PageModel Page { get; set; }
    }
}
