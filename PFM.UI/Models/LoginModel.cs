using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your User Code.")]
        public string UserCode { get; set; }=string.Empty;

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }= string.Empty;
    }
}
