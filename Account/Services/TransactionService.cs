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
        private static readonly object locker = new object();

        public TransactionService(BankDbContext bankDbContext, IMapper mapper, IAccountService account)
        {
            this.bankDbContext = bankDbContext;
            this.mapper = mapper;
            this.accountService = account;
        }

        public PagedResponse<TransactionDto> GetTransactions(TransactionParameter parameters)
        {
            var query = GetTransactionQuery()
                .Where(acc => acc.AccountId == parameters.AccountId)
                .OrderByDescending(t => t.EntryDate);

            var result = PagedResponse<Transaction>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);

            return mapper.Map<PagedResponse<TransactionDto>>(result);
        }

        public PagedResponse<TransactionDto> GetTransactionsForCustomer(CustomerTransactionParameter parameters)
        {
            var query = GetTransactionQuery()
                .Join(bankDbContext.CustomerAccountMappings, t => t.AccountId, c => c.AccountId, (t, c) => new { t, c })
                .Where(tc => tc.c.CustomerId == parameters.CustomerID);

            if (parameters.StartDate != null)
            {
                query = query.Where(tc => tc.t.EntryDate >= parameters.StartDate);
            }

            if (parameters.EndDate != null)
            {
                query = query.Where(tc => tc.t.EntryDate <= parameters.EndDate);
            }

            var result = PagedResponse<Transaction>.ToPagedList(query.Select(tc => tc.t).OrderByDescending(t => t.EntryDate), parameters.PageNumber, parameters.PageSize);

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

                if (account.Balance < 0)
                    throw new System.Exception("Low Account Balance: Cannot procced with the transaction");

                bankDbContext.Accounts.Update(mapper.Map<Account>(account));

                bankDbContext.SaveChanges();
            }

            var newlyCreatedTransaction = await GetTransactionQuery()
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

        private IQueryable<Transaction> GetTransactionQuery()
        {
            return bankDbContext.Transactions.AsNoTracking()
                .Include(t => t.Account)
                .Include(t => t.Account.Customers.Customer);
        }
    }
}
