using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Data.Entity
{
    public class UserInfo
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public string CreitCard { get; set; }
        
    }
}
