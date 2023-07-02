using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class RegestrationVM
    {
        [Required]
        [MinLength(6, ErrorMessage ="UserName Length Not Enough!")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter Valid Email!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage ="Password Minumum Length is 6 ")]
        public String Password { get; set; }
        [Required]
        public string? Role { get; set; }
        public  IEnumerable<RolesVM> Roles { get; set; }

    }
}
