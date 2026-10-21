using PFM.Domain.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Domain.Entites
{
    public class Categories :AuditeEntityes
    {
        public int CategoriesId { get; set; }
        public int UserId { get; set; }
        public string CategoriesName { get; set; } = string.Empty;
        public string CategoriesCode { get; set; }=string.Empty;
        
    }
}
