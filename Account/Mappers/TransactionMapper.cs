using AutoMapper;
using BankLibrary.Data.Tables;
using BankLibrary.DTOs;
using System;

namespace BankLibrary.Mappers
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<AddTransactionDto, TransactionDto>()
                .BeforeMap((s, d) => d.EntryDate = DateTime.Now);

            CreateMap<TransactionDto, Transaction>();

            CreateMap<Transaction, TransactionDto>()
                .AfterMap((s, d) => d.CustomerName = $"{s.Account.Customers.Customer.FirstName} {s.Account.Customers.Customer.LastName}")
                .AfterMap((s, d) => d.CustomerID = s.Account.Customers.CustomerId)
                .AfterMap((s, d) => d.AccountBalance = s.Account.Balance);
        }
    }
}
