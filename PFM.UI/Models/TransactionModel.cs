using System.ComponentModel.DataAnnotations;

namespace PFM.UI.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public int CategoriesId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
