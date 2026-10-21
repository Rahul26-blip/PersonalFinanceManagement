using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class InvestmentModel
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Investment Name is required.")]
        public string InvestmentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Investment Type is required.")]
        public string InvestmentType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Invested Amount is required.")]
        public int InvestedAmount { get; set; }

        [Required(ErrorMessage = "Current Value is required.")]
        public int CurrentValue { get; set; }

        [Required(ErrorMessage = "Investment Date is required.")]
        public DateOnly InvestmentDate { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
