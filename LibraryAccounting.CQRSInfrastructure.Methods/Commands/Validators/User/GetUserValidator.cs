using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(u => u.Id).NotEqual(0);
        }
    }
}
