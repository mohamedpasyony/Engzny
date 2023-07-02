using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(2500)]
        public string Description { get; set;}
        public byte[] Image { get; set; }
        public virtual ICollection<Craftman> Craftmens { get; set; }
        public virtual ICollection<Order> Orders { get; set; }



    }
}
