using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class ResetPasswordVM
    {
        [EmailAddress(ErrorMessage = "Enter Valid Email!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password Minumum Length is 6 ")]
        public String Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password Minumum Length is 6 ")]
        [Compare("Password" , ErrorMessage ="Not Macthing")]
        public String ConfirmPassword { get; set; }
        public String Token { get; set; }

    }
}
