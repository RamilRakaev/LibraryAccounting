using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class ChangingAllUserPropertiesValidator : AbstractValidator<ChangingAllPropertiesCommand>
    {
        public ChangingAllUserPropertiesValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Name).NotEmpty().Length(3, 20);
            RuleFor(c => c.RoleId).NotEmpty();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(10);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
        }
    }
}
