using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Account : AuditeEntityes
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public int CurrentBalance { get; set; }
        

    }
}
