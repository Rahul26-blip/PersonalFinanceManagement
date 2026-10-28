using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFM.Application.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserCode { get; set; }=string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
