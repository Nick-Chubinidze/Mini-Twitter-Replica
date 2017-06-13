using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Twitter_Nika_Chubinidze.Models
{  

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } 

        [Required, MaxLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    } 

    public class RegisterAndLoginViewModel
    {
        public LoginViewModel Login { get; set; }

        public RegisterViewModel Register { get; set; } 
    }
}
