using AutoMapper;
using BankLibrary.Data.Tables;
using BankLibrary.DTOs;

namespace BankLibrary.Mappers
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, AccountDto>()
                .AfterMap((s, d) => d.DisplayName = $"{s.Customers.Customer.FirstName} {s.Customers.Customer.LastName}")
                .AfterMap((s, d) => d.DateOfBirth = s.Customers.Customer.DateOfBirth.ToShortDateString())
                .AfterMap((s, d) => d.Address = s.Customers.Customer.Address)
                .AfterMap((s, d) => d.Gender = s.Customers.Customer.Gender)
                .AfterMap((s, d) => d.CustomerId = s.Customers.Customer.Id);

            CreateMap<AccountDto, Account>();

            CreateMap<CreateAccountDto, AccountDto>()
                .BeforeMap((s, d) => d.CloseDate = null);
        }

    }
}
