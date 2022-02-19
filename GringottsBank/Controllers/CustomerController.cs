using BankLibrary.DTOs;
using BankLibrary.RequestParameters;
using GringottsBank.Managers;
using GringottsBank.Token.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [JWTAuth]
    public class CustomerController : BaseController
    {
        private readonly CustomerManager customerManager;

        public CustomerController(CustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] Parameters parameters)
        {
            var respone = await customerManager.GetCustomers(parameters);

            return EndpointResponse(respone);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var respone = await customerManager.GetCustomer(id);

            return EndpointResponse(respone);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CreateCustomerDto dto)
        {
            var respone = await customerManager.AddCustomer(dto);

            return EndpointResponse(respone);
        }

    }
}
