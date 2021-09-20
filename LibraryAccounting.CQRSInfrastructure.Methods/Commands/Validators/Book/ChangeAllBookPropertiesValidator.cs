using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators.Book
{
    class ChangeAllBookPropertiesValidator : AbstractValidator<ChangeAllBookPropertiesCommand>
    {
        public ChangeAllBookPropertiesValidator()
        {
            RuleFor(b => b.Book).NotNull();
            RuleFor(b => b.Book.Id).NotEqual(0);
            RuleFor(b => b.Book.Title).NotNull().NotEmpty();
            RuleFor(b => b.Book.Publisher).NotNull().NotEmpty();
            RuleFor(b => b.Book.AuthorId).NotEqual(0);
            RuleFor(b => b.Book.GenreId).NotEqual(0);
        }
    }
}
