using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class CategoryModel
    {
        public int CategoriesId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoriesName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category Code is required.")]
        public string CategoriesCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
