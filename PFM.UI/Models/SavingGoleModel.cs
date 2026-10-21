namespace PFM.UI.Models
{
    public class SavingGoleModel
    {
        public int SavingsGolesId { get; set; }
        public int UserId { get; set; }
        public string GoleName { get; set; } = string.Empty;
        public int TargetAmount { get; set; }
        public int CurrentAmount { get; set; }
        public DateOnly TargetDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
