using System;
using System.Collections.Generic;

namespace BankLibrary.DTOs
{
    public class CreateAccountDto
    {
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }
    }
}
