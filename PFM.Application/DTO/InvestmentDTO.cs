using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFM.Application.DTO
{
    public class InvestmentDTO
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public string InvestmentName { get; set; } = string.Empty;
        public string InvestmentType { get; set; } = string.Empty;
        public int InvestedAmount { get; set; }
        public int CurrentValue { get; set; }
        public DateOnly InvestmentDate { get; set; }

        [Required(ErrorMessage = "Click The CheckBox")]
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
