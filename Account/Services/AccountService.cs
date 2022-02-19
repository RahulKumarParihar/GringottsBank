using AutoMapper;
using BankLibrary.Abstracts;
using BankLibrary.Data;
using BankLibrary.Data.Tables;
using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankDbContext bankDbContext;
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;

        public AccountService(BankDbContext bankDbContext, IMapper mapper, ICustomerService customerService)
        {
            this.bankDbContext = bankDbContext;
            this.mapper = mapper;
            this.customerService = customerService;
        }

        public PagedResponse<AccountDto> GetAccounts(Parameters parameters)
        {
            var query = GetAccountQuery();

            var result = PagedResponse<Account>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);

            return mapper.Map<PagedResponse<AccountDto>>(result);
        }

        public async Task<AccountDto> GetAccount(int id)
        {
            var query = GetAccountQuery();

            query = query.Where(cust => cust.Id == id);

            var result = await query.SingleOrDefaultAsync();

            return mapper.Map<AccountDto>(result);
        }

        public async Task<AccountDto> AddAccount(AccountDto accountDto)
        {
            var customer = await customerService.GetCustomer(accountDto.CustomerId);

            if(customer is null)
            {
                throw new System.Exception("Invalid Customer");
            }

            var newAccount = mapper.Map<Account>(accountDto);
            bankDbContext.Accounts.Add(newAccount);
            newAccount.Customers = new CustomerAccount { CustomerId = customer.Id };
            await bankDbContext.SaveChangesAsync();

            return await GetAccount(newAccount.Id);
        }

        public async Task<AccountDto> UpdateAccount(AccountDto accountDto)
        {
            var existingAccount = mapper.Map<Account>(accountDto);
            
            bankDbContext.Accounts.Update(existingAccount);
            await bankDbContext.SaveChangesAsync();

            return await GetAccount(existingAccount.Id);
        }

        private IQueryable<Account> GetAccountQuery()
        {
            return bankDbContext.Accounts.AsNoTracking()
                .Include(a => a.Customers)
                .Include(a => a.Customers.Customer);
        }
    }
}
