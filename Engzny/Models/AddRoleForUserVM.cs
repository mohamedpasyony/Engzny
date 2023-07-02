using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class AddRoleForUserVM
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Role { get; set; }
        public IEnumerable<RolesVM> Roles { get; set; }
    }
}
