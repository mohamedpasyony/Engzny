using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class Craftman
    {
        public int Id { get; set;}

        [Required, MaxLength(50)]
        public string Name { get; set;}
        public byte Age { get; set; }
        [Required, MaxLength(2500)]
        public string Address { get; set; }
        public int Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public string Notes { get; set; }
        public byte ServiceId { get; set; }
        public Service Service { get; set; }
        public byte CityId { get; set; }
        public City City { get; set; }


    }
}
