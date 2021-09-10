using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public GetUsersHandler(UserManager<ApplicationUser> _db) : base(_db)
        { }

        private List<ApplicationUser> Users;
        async Task<List<ApplicationUser>> IRequestHandler<GetUsersQuery, List<ApplicationUser>>.Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            Users = _db.Users.ToList();
            await Task.Run(() => Filter(request));
            return Users;
        }

        private void Filter(GetUsersQuery request)
        {
            var userProperties = typeof(ApplicationUser).GetProperties();
            var userPropertyNames = userProperties.Select(p => p.Name);

            foreach (var requestProperty in typeof(GetUsersQuery).GetProperties())
            {
                if (requestProperty.GetValue(request) != null)
                    if (userPropertyNames.Contains(requestProperty.Name))
                    {
                        for (int i = 0; i < Users.Count(); i++)
                        {
                            var userProperty = userProperties.FirstOrDefault(u => u.Name == requestProperty.Name);
                            if (userProperty.GetValue(Users[i]).ToString() != requestProperty.GetValue(request).ToString())
                            {
                                Users.Remove(Users[i]);
                            }
                        }
                    }

            }
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
