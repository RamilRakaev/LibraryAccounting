using FluentValidation;
using LibraryAccounting.Domain.Model;

namespace LibraryAccounting.Infrastructure.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.UserName).Length(3, 20);
            RuleFor(p => p.Email).EmailAddress().NotNull();
            RuleFor(p => p.Password).MinimumLength(10).NotNull();
        }
    }
}
