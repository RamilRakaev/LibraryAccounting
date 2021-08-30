using FluentValidation;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Validator
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.Id).NotNull();
            RuleFor(b => b.BookId).NotNull();
            RuleFor(b => b.ClientId).NotNull();
            RuleFor(b => b.TransferDate).NotNull().When(b => b.IsTransmitted);
            RuleFor(b => b.ReturnDate).NotNull().When(b => b.IsReturned);
        }
    }
}
