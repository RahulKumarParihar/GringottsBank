using GringottsBank.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace GringottsBank.Token.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JWTAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var tokenUtil = (JWTTokenUtil)context.HttpContext.RequestServices.GetService(typeof(JWTTokenUtil));
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            authorizationHeader = authorizationHeader.ToString().Replace("Bearer ", "");
            var isValid = tokenUtil.ValidateJwtToken(authorizationHeader);

            if (!isValid)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
