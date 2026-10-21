using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class User :AuditeEntityes
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserCode { get; set; }=string.Empty;
        public string? PasswordHash { get; set; }=string.Empty;
    }
}
