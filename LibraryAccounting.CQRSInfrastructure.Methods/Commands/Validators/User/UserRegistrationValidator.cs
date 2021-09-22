using FluentValidation;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests.User;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationCommand>
    {
        public UserRegistrationValidator()
        {
            RuleFor(u => u.UserName)
                .NotNull()
                .NotEmpty()
                .Length(3, 50)
                .WithMessage("Неправильно введено имя");

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("Неправильно введёна почта");

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Неправильно введён пароль");

            RuleFor(u => u.PasswordConfirm)
                .NotNull()
                .NotEmpty()
                .Equal(u => u.Password)
                .WithMessage("Пароли не совпадают");

            RuleFor(u => u.RoleId).NotEqual(0);
        }
    }
}
