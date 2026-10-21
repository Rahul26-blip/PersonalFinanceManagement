using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class RolePermission :AuditeEntityes
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        
    }
}
