using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class AddAuthorValidator : AbstractValidator<AddAuthorCommand>
    {
        public AddAuthorValidator()
        {
            RuleFor(g => g.Name).NotNull().NotEmpty();
        }
    }
}
