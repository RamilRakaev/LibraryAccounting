using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class RemoveBookingValidator : AbstractValidator<RemoveBookingCommand>
    {
        public RemoveBookingValidator()
        {
            RuleFor(b => b.Id).NotNull().NotEqual(0);
        }
    }
}
