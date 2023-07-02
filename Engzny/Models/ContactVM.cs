using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Engzny.Models
{
    public class ContactVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب*"), StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage ="هذا الحقل مطلوب*")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "من فضلك ادخل رقم صحيح")]
        public int Phone { get; set;}

        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "من فضلك ادخل عنوان بريد الكتروني صحيح")]
        public string Email { get; set; }
        public DateTime ContactDateTime { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "هذا الحقل مطلوب*")]
        public string Message { get; set; }


    }
}
