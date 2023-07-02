using Engzny.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime AnotherDate { get; set; }
        [Required]
        public DateTime OrderDateTime { get; set; }
        [Required]
        public string Address { get; set; }

        public string FeedBack { get; set; }

        public string Notes { get; set; }

        public byte ServiceId { get; set; }
        public Service Service { get; set; }
        public int StatusId { get; set; }
        public  OrderStatus Status { get; set; }

    }
}
