using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Comman
{
    public class AuditeEntityes
    {
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
