using System.ComponentModel.DataAnnotations;

namespace LibraryAccounting.Pages.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
