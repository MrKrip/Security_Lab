using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Data.Entity
{
    public class UserInfo
    {
        [Required]
        public int Id { get; set; }
        [ForeignKey("Id")]
        public User User { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }
        public string Nonce { get; set; }
        public string Tag { get; set; }
    }
}
