using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Transaction :AuditeEntityes
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public int CategoriesId { get; set; }
        public string TransactionType { get; set; }=string.Empty;
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }= string.Empty;
        
    }
}
