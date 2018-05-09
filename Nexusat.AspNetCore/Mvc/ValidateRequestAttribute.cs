using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Nexusat.AspNetCore.Mvc
{
    public class ValidateRequestAttribute : ActionFilterAttribute
    {
        private readonly ILogger logger;
        public ValidateRequestAttribute(ILogger<ValidateRequestAttribute> logger)
        {
            this.logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
            /*
            if (!context.ModelState.IsValid)
            {
                logger.LogDebug("Request was not valid");
                context.Result = new BadRequestObjectResult(
                    new ModelBadRequestResponse(context.HttpContext, context.ModelState));
            }
            */
        }
    }
}
