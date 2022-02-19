using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLibrary.Data.Tables
{
    [Table("Account")]
    public class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        public int Id { get; set; }
        [Column("branch_id")]
        public int? BranchId { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required, Column("create_date")]
        public DateTime CreateDate { get; set; }
        [Column("close_date")]
        public DateTime? CloseDate { get; set; }

        public virtual CustomerAccount Customers { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
