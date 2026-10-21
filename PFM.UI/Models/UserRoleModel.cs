using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class UserRoleModel
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
