using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class AddBookValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookValidator()
        {
            RuleFor(b => b.Book.Id).Equals(0);
        }
    }
}
