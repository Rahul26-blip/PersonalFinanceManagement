using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFM.Domain.Entites
{
    public class SavingGole :AuditeEntityes
    {
        [Key]
        public int SavingsGolesId { get; set; }
        public int UserId { get; set; }
        public string GoleName { get; set; }= string.Empty;
        public int TargetAmount { get; set; }
        public int CurrentAmount { get; set; }
        public DateOnly TargetDate { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}
