using BankLibrary.DTOs;
using BankLibrary.RequestParameters;
using GringottsBank.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly TransactionManager transaction;

        public TransactionController(TransactionManager transaction)
        {
            this.transaction = transaction;
        }

        [HttpGet("account")]
        public async Task<IActionResult> GetAccountTransaction([FromQuery] TransactionParameter parameter)
        {
            var respone = await transaction.GetTransactionForAccounts(parameter);

            return EndpointResponse(respone);
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerTransaction([FromQuery] CustomerTransactionParameter parameter)
        {
            var respone = await transaction.GetTransactionsForCustomer(parameter);

            return EndpointResponse(respone);
        }

        [HttpPost("credit")]
        public async Task<IActionResult> Credit(AddTransactionDto dto)
        {
            var respone = await transaction.Credit(dto);

            return EndpointResponse(respone);
        }

        [HttpPost("debit")]
        public async Task<IActionResult> Debit(AddTransactionDto dto)
        {
            var respone = await transaction.Debit(dto);

            return EndpointResponse(respone);
        }
    }
}
