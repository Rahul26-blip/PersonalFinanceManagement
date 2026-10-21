using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "This field is incomplete.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is incomplete.")]
        public string UserCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
