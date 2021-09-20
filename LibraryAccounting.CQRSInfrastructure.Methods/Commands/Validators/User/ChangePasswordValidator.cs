using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.Password).NotEmpty().MinimumLength(10);
        }
    }
}
