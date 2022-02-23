using GringottsBank.Models;
using GringottsBank.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Controllers
{
    [Route("api/token")]
    [ApiController]
    [Token.Attributes.JWTAuth]
    public class TokenController : BaseController
    {
        private readonly JWTTokenUtil tokenUtil;

        public TokenController(JWTTokenUtil tokenUtil)
        {
            this.tokenUtil = tokenUtil;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetToken()
        {
            var respone = new Success<string>
            {
                Data = tokenUtil.GenerateJwtToken()
            };

            return EndpointResponse(respone);
        }
    }
}
