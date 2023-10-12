using Microsoft.AspNetCore.Mvc.Filters;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Mvc;

public class ValidateRequestAttribute : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		if (!context.ModelState.IsValid)
			context.Result = new BadRequest.Response(context.ModelState);
	}
}