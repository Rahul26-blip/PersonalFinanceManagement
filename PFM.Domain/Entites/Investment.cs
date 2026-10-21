using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
     public class Investment:AuditeEntityes
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; }
        public string InvestmentName { get; set; }=string.Empty;
        public string InvestmentType { get; set; }=string.Empty;
        public int InvestedAmount { get; set; }
        public int CurrentValue { get; set; }
        public DateOnly InvestmentDate { get; set; }
    }
    
}
