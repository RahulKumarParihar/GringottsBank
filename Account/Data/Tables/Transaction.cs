using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLibrary.Data.Tables
{
    [Table("AccountTransaction")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required, Column("entry_date")]
        public DateTime EntryDate { get; set; }
        [Required, Column("account_id")]
        public int AccountId { get; set; }
        [Required, StringLength(1)]
        public String Type { get; set; }

        public virtual Account Account { get; set; }
    }
}
