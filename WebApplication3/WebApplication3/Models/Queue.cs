using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Queue
    {
        
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]
        public string PID { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]
        public string DID { get; set; }

        [Key]
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        
        //
        public bool mode { get;set; }

    }
}