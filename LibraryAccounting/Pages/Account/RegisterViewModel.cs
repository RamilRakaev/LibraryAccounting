using System.ComponentModel.DataAnnotations;

namespace LibraryAccounting.Pages.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Вы не ввели имя")]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вы не ввели почту")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не ввели пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Вы не подтвердили пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
