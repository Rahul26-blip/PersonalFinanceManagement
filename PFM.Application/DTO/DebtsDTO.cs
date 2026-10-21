using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFM.Application.DTO
{
    public class DebtsDTO
    {
        public int DebtsId { get; set; }
        public int UserId { get; set; }
        public string DebtsName { get; set; } = string.Empty;
        public int ToltalAmount { get; set; }
        public int RemainingAmount { get; set; }
        public int? InterestRate { get; set; }
        public DateOnly? DueDate { get; set; }
        public bool IsPaid { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
