using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Login2.Models
{
    public class LoginF
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s().-]*$", ErrorMessage = "Name can only contain letters, spaces, (, ), . and -.")]
        public string name { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "ID must be between 4 and 12 characters.")]
        [RegularExpression(@"^[0-9_-]*$", ErrorMessage = "ID can only contain numbers, dash, and underscore.")]
        public string id { get; set; }

        [Required]
        [Passwordd(ErrorMessage = "Password must have at least 8 characters. The first 4 must be alphabets (1 upper, 2 lower), and the next 4 must be a combination of special characters and numbers.")]
        public string password { get; set; }
       
        [Required]
        [AIUB(ErrorMessage = "Error")]
        public string Aid { get; set; }

        [Required]
        [Email(ErrorMessage = "Error email")]
        public string email { get; set; }
        [Required]
        [MinimumAge(18, ErrorMessage = "Must be at least 18 years old.")]
        public DateTime dob { get; set; }
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Invalid gender.")]

       
        public string gender { get; set; }
        [Required(ErrorMessage = "Profession is required.")]
        public string profession { get; set; }
        [Hobbies(ErrorMessage = "Selecting one hobby is accepted.")]
        public string[] hobbies { get; set; }
    }
}