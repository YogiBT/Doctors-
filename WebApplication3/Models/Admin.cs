using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Admin
    {

        [Key]
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]

        public string userName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]
        public string password { get; set; }
    }
}