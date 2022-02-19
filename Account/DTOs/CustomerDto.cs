using System;

namespace BankLibrary.DTOs
{
    public class CustomerDto : CreateCustomerDto
    {
        public int Id { get; set; }
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }
    }
}
