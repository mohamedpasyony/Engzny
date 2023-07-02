using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class CraftmanVM
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        [Range(18,80)]
        public byte Age { get; set; }
        [Required, StringLength(2500)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
         [RegularExpression(@"^01[0125][0-9]{8}$",ErrorMessage ="Please enter valid number")]
        public int Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Count { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Service Type")]
        public byte ServiceId { get; set; }
        public Service Service { get; set; }
        [Display(Name = "City")]

        public byte CityId { get; set; }
        public City City { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<City> Cities { get; set; }


    }
}
