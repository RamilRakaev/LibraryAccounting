using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class ConfirmEmailCommand : IRequest<ApplicationUser>
    {
        public string Email { get; set; }
        public string Code { get; set; }

        public ConfirmEmailCommand(string email, 
            string code)
        {
            Email = email;
            Code = code;
        }
    }
}
