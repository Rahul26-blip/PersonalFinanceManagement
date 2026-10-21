using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Role : AuditeEntityes
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string RoleCode { get; set; } = string.Empty;

    }
}
