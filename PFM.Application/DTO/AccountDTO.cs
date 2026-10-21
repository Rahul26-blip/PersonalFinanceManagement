using System.ComponentModel.DataAnnotations;

namespace PFM.Application.DTO
{
    public class AccountDTO
    {
        
        public int AccountId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "This field is incomplete.")]
        public string AccountName { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is incomplete.")]
        public string AccountType { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is incomplete.")]
        public int CurrentBalance { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
