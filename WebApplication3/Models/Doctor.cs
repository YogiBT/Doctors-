using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Doctor
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]
        public string LastName { get; set; }
        [Key]
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]

        public string DID { get; set; }

        [Required]
        //[RegularExpression("^[0-9]{3}$",ErrorMessage ="Enter Valid age")]

        public int Age { get; set; }


        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]

        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Valid email is required")]

        public string Email { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Input must be 3 to 15 characters")]
        public string Password { get; set; }
    }
}
