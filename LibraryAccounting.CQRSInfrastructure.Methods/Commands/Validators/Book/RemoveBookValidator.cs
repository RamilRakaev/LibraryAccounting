using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class RemoveBookValidator : AbstractValidator<RemoveBookCommand>
    {
        public RemoveBookValidator()
        {
            RuleFor(b => b.Id).NotEqual(0);
        }
    }
}
