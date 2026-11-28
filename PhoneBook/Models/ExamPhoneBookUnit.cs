using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    [Table("TM_AllJobs",Schema="dbo")]
    public class ExamPhoneBookUnit
    {
        [Column("Job_Id")]
        public int Id { get; set; }
        [Column ("Job_Title")]
        public string Title { get; set; }
        [Column("Job_Full_Name")]
        public string FullNamae { get; set; }
    }
}
