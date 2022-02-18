using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using System.Threading.Tasks;

namespace BankLibrary.Abstracts
{
    public interface IAccountService
    {
        Task<AccountDto> AddAccount(AccountDto accountDto);
        Task<AccountDto> GetAccount(int id);
        PagedResponse<AccountDto> GetAccounts(Parameters parameters);
        Task<AccountDto> UpdateAccount(AccountDto accountDto);
    }
}
