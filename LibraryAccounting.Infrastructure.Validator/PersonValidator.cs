using FluentValidation;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Validator
{
    public class PersonValidator : AbstractValidator<User>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Name).Length(3, 20);
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.Password).MinimumLength(10);
        }
    }
}
