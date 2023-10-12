using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc.Formatters;

namespace Nexusat.AspNetCore.Middleware;

/// <summary>
/// Custom status codes middleware.
/// Responsible to manage HTTP responses for HTTP manage pipeline (404, 415, …)
/// </summary>
class CustomStatusCodesMiddleware
{
	private readonly RequestDelegate next;
	private ILogger<CustomStatusCodesMiddleware> logger;

	public CustomStatusCodesMiddleware(RequestDelegate next, ILogger<CustomStatusCodesMiddleware> logger)
	{
		this.next = next;
		this.logger = logger;
	}

	public async Task Invoke(HttpContext context /* other dependencies */)
	{
		void RenderResponse(ApiResponse response)
		{
			var services = context.RequestServices;
			var executor = services.GetRequiredService<ApiResponseExecutor>();
			executor.RenderResponse(context, response);
		}

		await next(context);

		switch(context.Response.StatusCode) {
		case 404:
			RenderResponse(new NotFoundResponse());
			break;
		case 415:
			RenderResponse(new UnsupportedMediaTypeResponse());
			break;  
		}         
	}
}