using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankLibrary.Data.Tables
{
    public class CustomerAccount
    {
        [Required, Column("customer_id")]
        public int CustomerId { get; set; }
        [Required, Column("account_id")]
        public int AccountId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Account Account { get; set; }
    }
}
