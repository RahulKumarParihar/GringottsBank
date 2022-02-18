using System;

namespace BankLibrary.DTOs
{
    public class AccountDto : CreateAccountDto
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public decimal Balance { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
