using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    [Table("TM_PhoneBookEntry",Schema="dbo")]
    public class ExamPhoneBookEntry
    {
        [Key]
        [Column("PBE_Id")]
        public string Id { get; set; }
        [Column("PBE_FirstName")]
        public string FirstName { get; set; }
        [Column("PBE_LastName")]
        public string LastName { get; set; }
        [Column("PBE_Unit")]
        public string Unit { get; set; }
        [Column("PBE_InternalPhone")]
        public string InternalPhone { get; set; }
        [Column("PBE_PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Column("PBE_MobileNumber")]
        public string MobileNumber { get; set; }
        [Column("PBE_Is_deleted")]
        public bool IsDeleted { get; set; }
        [Column("PBE_Version")]
        public long Version { get; set; }
        [Column("PBE_CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("PBE_CreatedByUserId_Fk")]
        public string CreatedByUserId { get; set; }
        
    }
}
