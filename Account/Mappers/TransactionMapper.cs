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

            CreateMap<Transaction, TransactionDto>();
        }
    }
}
