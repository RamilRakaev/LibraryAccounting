using FluentValidation;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Validator
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Id).NotNull();
            RuleFor(b => b.Title).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        }
    }
}
