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
    public class TransactionService : ITransactionService
    {
        private readonly BankDbContext bankDbContext;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly ICustomerService customer;
        private static readonly object locker = new object();

        public TransactionService(BankDbContext bankDbContext, IMapper mapper, IAccountService account, ICustomerService customer)
        {
            this.bankDbContext = bankDbContext;
            this.mapper = mapper;
            this.accountService = account;
            this.customer = customer;
        }

        public PagedResponse<TransactionDto> GetTransactions(TransactionParameter parameters)
        {
            var query = bankDbContext.Transactions.AsNoTracking().Where(acc => acc.AccountId == parameters.AccountId);

            var result = PagedResponse<Transaction>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);

            return mapper.Map<PagedResponse<TransactionDto>>(result);
        }

        public PagedResponse<TransactionDto> GetTransactionsForCustomer(CustomerTransactionParameter parameters)
        {
            var query = bankDbContext.Transactions.AsNoTracking().Join(bankDbContext.CustomerAccountMappings, t => t.AccountId, c => c.AccountId, (t, c) => new { t, c })
                .Where(tc => tc.c.CustomerId == parameters.CustomerID);

            if (parameters.StartDate != null)
            {
                query = query.Where(tc => tc.t.EntryDate >= parameters.StartDate);
            }

            if (parameters.EndDate != null)
            {
                query = query.Where(tc => tc.t.EntryDate <= parameters.EndDate);
            }

            var result = PagedResponse<Transaction>.ToPagedList(query.Select(tc => tc.t), parameters.PageNumber, parameters.PageSize);

            return mapper.Map<PagedResponse<TransactionDto>>(result);
        }

        public async Task<TransactionDto> AddTransaction(TransactionDto addTransaction)
        {
            var account = await accountService.GetAccount(addTransaction.AccountId);

            if (account is null)
            {
                throw new System.Exception("Invalid Account");
            }
            decimal accountBalance = account.Balance;

            var newTransaction = mapper.Map<Transaction>(addTransaction);

            bankDbContext.Transactions.Add(newTransaction);

            lock (locker)
            {
                UpdateAccountBalance(addTransaction, account);

                bankDbContext.Accounts.Update(mapper.Map<Account>(account));

                bankDbContext.SaveChanges();
            }

            var newlyCreatedTransaction = await bankDbContext.Transactions
                .SingleOrDefaultAsync(acc => acc.Id == newTransaction.Id);

            return mapper.Map<TransactionDto>(newlyCreatedTransaction);
        }

        private void UpdateAccountBalance(TransactionDto addTransaction, AccountDto accountDto)
        {
            if (addTransaction.Type == "D")
            {
                accountDto.Balance -= addTransaction.Amount;
            }
            else
            {
                accountDto.Balance += addTransaction.Amount;
            }
        }

        //public async Task<AccountDto> UpdateAccount(AccountDto accountDto)
        //{
        //    var existingAccount = mapper.Map<Account>(accountDto);
        //    bankDbContext.Accounts.Update(existingAccount);

        //    await bankDbContext.SaveChangesAsync();

        //    var newlyCreatedAccount = await bankDbContext.Accounts.SingleOrDefaultAsync(acc => acc.Id == existingAccount.Id);

        //    return mapper.Map<AccountDto>(newlyCreatedAccount);
        //}
    }
}
