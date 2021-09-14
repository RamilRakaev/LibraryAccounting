using System.ComponentModel.DataAnnotations;

namespace LibraryAccounting.Pages.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Вы не ввели имя")]
        [Display(Name = "Имя")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не ввели пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
