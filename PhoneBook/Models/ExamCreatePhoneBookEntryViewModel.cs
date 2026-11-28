using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class ExamCreatePhoneBookEntryViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UnitId { get; set; }
        [Required]
        public string InternalPhone { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
    }
}
