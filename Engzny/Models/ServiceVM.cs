using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class ServiceVM
    {
        public byte Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(2500)]
        public string Description { get; set; }
        [Display(Name ="Choose Image")]
        public byte[] Image { get; set; }
        public byte Count { get; set; }
    }
}
