using System.ComponentModel.DataAnnotations;

namespace PFM.Application.DTO
{
    public class BudgetDTO
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public int CategoriesId { get; set; }

        [Required(ErrorMessage = "This field is incomplete.")]
        public int Amount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
