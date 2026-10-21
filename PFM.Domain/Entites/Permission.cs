using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Permission :AuditeEntityes
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public string PermissionCode { get; set; }=string.Empty;
        
    }
}
