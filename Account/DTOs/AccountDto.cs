using System;

namespace BankLibrary.DTOs
{
    public class AccountDto : CreateAccountDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string DisplayName { get; set; } 
    }
}
