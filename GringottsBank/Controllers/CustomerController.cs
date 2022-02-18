using BankLibrary.RequestParameters;
using GringottsBank.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GringottsBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly CustomerManager customerManager;

        public CustomerController(CustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetCustomers([FromQuery] Parameters parameters)
        {
            var respone = await customerManager.GetCustomers(parameters);

            return EndpointResponse(respone);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var respone = await customerManager.GetCustomer(id);

            return EndpointResponse(respone);
        }


    }
}
