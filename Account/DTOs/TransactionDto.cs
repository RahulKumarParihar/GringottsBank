using System;

namespace BankLibrary.DTOs
{
    public class TransactionDto : AddTransactionDto
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Type { get; set; }
        public int CustomerID { get; set; }
        public decimal AccountBalance { get; set; }
        public string CustomerName { get; set; }
    }
}
