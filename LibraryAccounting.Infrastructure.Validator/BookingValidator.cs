using Domain.Model;
using FluentValidation;

namespace LibraryAccounting.Infrastructure.Validator
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.Id).NotNull();
            RuleFor(b => b.BookId).NotNull();
            RuleFor(b => b.PersonAccountId).NotNull();
            RuleFor(b => b.TransferDate).NotNull();
            RuleFor(b => b.ReturnDate).NotNull().When(b => b.IsReturned);
        }
    }
}
