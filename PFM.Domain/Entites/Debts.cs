using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Debts:AuditeEntityes
    {
        public int DebtsId { get; set; }
        public int UserId { get; set; }
        public string DebtsName { get; set; }=string.Empty;
        public int ToltalAmount { get; set; }
        public int RemainingAmount { get; set; }
        public int? InterestRate { get; set; }
        public DateOnly? DueDate { get; set; }
        public bool IsPaid { get; set; }
        
    }
}
