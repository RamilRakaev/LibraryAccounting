﻿using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User
{
    public class UserRegistrationCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
        public PageModel Page { get; set; }
    }
}
