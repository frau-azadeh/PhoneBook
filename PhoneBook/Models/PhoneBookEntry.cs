using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PhoneBookEntry
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Unit { get; set; }
        public string InternalPhone { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public long Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByUserId { get; set;
        }
    }
}
