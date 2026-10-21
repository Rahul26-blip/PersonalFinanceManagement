using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your full name.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a User Code.")]
        public string UserCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
