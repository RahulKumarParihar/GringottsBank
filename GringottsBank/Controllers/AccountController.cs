using BankLibrary.DTOs;
using BankLibrary.RequestParameters;
using GringottsBank.Managers;
using GringottsBank.Token.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [JWTAuth]
    public class AccountController : BaseController
    {
        private readonly AccountManager accountManager;

        public AccountController(AccountManager accountManager)
        {
            this.accountManager = accountManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromQuery] Parameters parameters)
        {
            var respone = await accountManager.GetAccounts(parameters);

            return EndpointResponse(respone);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var respone = await accountManager.GetAccount(id);

            return EndpointResponse(respone);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(CreateAccountDto dto)
        {
            var respone = await accountManager.AddAccount(dto);

            return EndpointResponse(respone);
        }

        [HttpPatch("close/{id:int}")]
        public async Task<IActionResult> CloseAccount(int id)
        {
            var respone = await accountManager.CloseAccount(id);

            return EndpointResponse(respone);
        }
    }
}
