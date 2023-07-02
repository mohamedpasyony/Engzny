using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class OrderVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب*"), StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "من فضلك ادخل رقم صحيح")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "من فضلك ادخل عنوان بريد الكتروني صحيح")]
        public string Email { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب*")]

        public DateTime Date { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب*")]

        public DateTime AnotherDate { get; set; }

        public string Notes { get; set; }
        //read only prob
        public DateTime OrderDateTime { get; } = DateTime.Now;

        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        public string Address { get; set; }
        [Required(ErrorMessage = "FeedBack can't be empty !")]
        public string FeedBack { get; set; }

        public byte ServiceId { get; set; }
        public Service Service { get; set; }
        [Display(Name = "Status")]

        public int StatusId { get; set; } = 1;
        public OrderStatus Status { get; set;}
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<OrderStatus> statuses { get; set; }


    }
}
