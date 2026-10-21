using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Budget :AuditeEntityes
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public int CategoriesId { get; set; }
        public int Amount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        
    }
}
