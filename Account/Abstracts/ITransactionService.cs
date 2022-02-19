using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using System.Threading.Tasks;

namespace BankLibrary.Abstracts
{
    public interface ITransactionService
    {
        Task<TransactionDto> AddTransaction(TransactionDto addTransaction);
        PagedResponse<TransactionDto> GetTransactions(TransactionParameter parameters);
        PagedResponse<TransactionDto> GetTransactionsForCustomer(CustomerTransactionParameter parameters);
    }
}
