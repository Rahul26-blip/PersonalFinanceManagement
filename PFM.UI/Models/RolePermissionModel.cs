using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class RolePermissionModel
    {
        public int RolePermissionId { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Permission is required.")]
        public int PermissionId { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
