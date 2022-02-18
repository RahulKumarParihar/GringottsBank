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
                    .ForMember(dest => dest.CustomerId, opts => opts.Ignore());

            CreateMap<AccountDto, Account>();

            CreateMap<CreateAccountDto, AccountDto>()
                .BeforeMap((s, d) => d.BranchId = null)
                .BeforeMap((s, d) => d.CloseDate = null);
        }

    }
}
