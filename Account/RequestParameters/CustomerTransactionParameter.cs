using System;
using System.ComponentModel.DataAnnotations;

namespace BankLibrary.RequestParameters
{
    public class CustomerTransactionParameter : Parameters
    {
        [Required]
        public int CustomerID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
