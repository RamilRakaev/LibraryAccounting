using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class RemoveAuthorValidator : AbstractValidator<RemoveAuthorCommand>
    {
        public RemoveAuthorValidator()
        {
            RuleFor(g => g.Id).NotEqual(0);
        }
    }
}
