using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class UsersVM
    {
        public string UserId { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "UserName Length Not Enough!")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter Valid Email!")]
        public string Email { get; set; }

    }
}
