using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.UserMethods
{
    public class GetUserQuery : IRequest<ApplicationUser>
    {
        public int Id { get; set; }
    }

    public class GetUserHandler : UserHandler, IRequestHandler<GetUserQuery, ApplicationUser>
    {
        public GetUserHandler(IRepository<ApplicationUser> _db) : base(_db)
        { }

        public async Task<ApplicationUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _db.FindAsync(request.Id);
        }
    }

    public class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(u => u.Id).NotEqual(0);
        }
    }
}
