using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators.User
{
    class UserRegistrationValidator : AbstractValidator<UserRegistrationCommand>
    {
        public UserRegistrationValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .Length(3, 50)
                .WithMessage("Неправильно введено имя");

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("Неправильно введёна почта");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(10)
                .Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$")
                .WithMessage("Неправильно введён пароль");

            RuleFor(u => u.RoleId).NotEqual(0);
        }
    }
}
