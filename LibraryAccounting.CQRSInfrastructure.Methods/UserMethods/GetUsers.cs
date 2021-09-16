using FluentValidation;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class GetUsersQuery : IRequest<List<ApplicationUser>>
    {
        public string Name { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class GetUsersHandler : UserHandler, IRequestHandler<GetUsersQuery, List<ApplicationUser>>
    {
        private List<ApplicationUser> users;

        public GetUsersHandler(UserManager<ApplicationUser> db) : base(db)
        { }

        async Task<List<ApplicationUser>> IRequestHandler<GetUsersQuery, List<ApplicationUser>>.Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            users = _db.Users.ToList();
            QueryFilter<ApplicationUser, GetUsersQuery> filter = 
                new QueryFilter<ApplicationUser, GetUsersQuery>(users);
            await Task.Run(() => filter.Filter(request));
            return users;
        }
    }


    public class GetUsersValidator : AbstractValidator<ApplicationUser>
    {
        public GetUsersValidator()
        {
            RuleFor(c => c.UserName).Length(3, 20);
            RuleFor(c => c.Password).MinimumLength(10);
            RuleFor(c => c.Email).EmailAddress();
        }
    }
}
