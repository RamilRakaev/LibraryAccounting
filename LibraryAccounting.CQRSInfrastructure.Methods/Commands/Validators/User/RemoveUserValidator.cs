using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class RemoveUserValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserValidator()
        {
            RuleFor(b => b.Id).NotNull().NotEqual(0);
        }
    }
}
