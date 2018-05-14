using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Mvc
{
    public class ValidateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
			if (!context.ModelState.IsValid)
				context.Result = new BadRequest.ApiResponse(context.ModelState);
        }
    }
}
