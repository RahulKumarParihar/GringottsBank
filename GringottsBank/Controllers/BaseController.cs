using GringottsBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult EndpointResponse<T>(ResponseModel<T> dto)
        {
            if (dto.HasError)
            {
                return BadRequest((Error<T>)dto);
            }
            else
            {
                return Ok((Success<T>)dto);
            }
        }
    }
}
