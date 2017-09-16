using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TJSystemWebUI.Models
{
    public class LogingUserModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Possword { get; set; }
    }
}