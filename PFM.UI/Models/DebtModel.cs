using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class DebtModel
    {
        public int DebtsId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Debt Name is required.")]
        public string DebtsName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Total Amount is required.")]
        public int ToltalAmount { get; set; }

        [Required(ErrorMessage = "Remaining Amount is required.")]
        public int RemainingAmount { get; set; }
        public int? InterestRate { get; set; }

        [Required(ErrorMessage = "Due Date is required.")]
        public DateOnly? DueDate { get; set; }
        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
