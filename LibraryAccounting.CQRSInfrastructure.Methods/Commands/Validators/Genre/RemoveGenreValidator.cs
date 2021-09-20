using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class RemoveGenreValidator : AbstractValidator<RemoveGenreCommand>
    {
        public RemoveGenreValidator()
        {
            RuleFor(g => g.Id).NotEqual(0);
        }
    }
}
