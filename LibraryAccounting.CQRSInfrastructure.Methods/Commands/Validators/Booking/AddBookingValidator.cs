using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class AddBookingValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingValidator()
        {
            RuleFor(b => b.BookId).NotEqual(0);
            RuleFor(b => b.ClientId).NotEqual(0);
        }
    }
}
