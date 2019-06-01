using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication3.Models
{
    public class UserReview
    {
        [Key]
        public int MessageID { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Message { get; set; }
        public string From { get; set; }
        

    }
}