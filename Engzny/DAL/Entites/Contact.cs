using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Phone { get; set; }
        [Required]
        public string Email { get; set;}
        [Required]
        public DateTime ContactDateTime { get; set; }
        [Required]
        public string Message  { get; set; }

    }
}
