﻿using System;

namespace BankLibrary.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }
    }
}
