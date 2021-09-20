using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class TransmissionAndAcceptanceBookValidator : AbstractValidator<TransmissionAndAcceptanceBookCommand>
    {
        public TransmissionAndAcceptanceBookValidator()
        {
            RuleFor(b => b.Id).NotEqual(0).When(b => b.BookId == 0 && b.ClientId == 0);
            RuleFor(b => b.BookId).NotEqual(0).When(b => b.Id == 0);
            RuleFor(b => b.ClientId).NotEqual(0).When(b => b.Id == 0);
        }
    }
}
