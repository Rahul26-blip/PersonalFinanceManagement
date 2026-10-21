using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter your full name.")]

        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a User Code.")]
        public string UserCode { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
