using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLibrary.Data.Tables
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<CustomerAccount>();
        }

        [Key]
        public int Id { get; set; }
        [Required, StringLength(50), Column("first_name")]
        public string FirstName { get; set; }
        [Required, StringLength(50), Column("last_name")]
        public string LastName { get; set; }
        [Required, StringLength(1)]
        public string Gender { get; set; }
        [Required, StringLength(200)]
        public string Address { get; set; }
        [Required, Column("dob")]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<CustomerAccount> Accounts { get; set; }
    }
}
