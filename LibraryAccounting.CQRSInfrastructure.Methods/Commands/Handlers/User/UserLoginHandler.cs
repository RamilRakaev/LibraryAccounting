using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers.User
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityResult>
    {
        public UserLoginHandler()
        {

        }

        public Task<IdentityResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
