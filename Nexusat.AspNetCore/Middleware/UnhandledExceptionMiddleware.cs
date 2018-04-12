using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Mvc.Formatters;

namespace Nexusat.AspNetCore.Middleware
{
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private ILogger<UnhandledExceptionMiddleware> logger;

        public UnhandledExceptionMiddleware(RequestDelegate next, ILogger<UnhandledExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogCritical("Unhandled Exception Caught! {0}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (!context.Response.HasStarted)
            {
                var services = context.RequestServices;
                var executor = services.GetRequiredService<ApiResponseExecutor>();
                var response = new UnhandledExceptionResponse(exception);
                executor.RenderResponse(context, response);
            } else {
                logger.LogCritical("Response already started. Unable to generate a response");
            }

            return Task.FromResult(0);
        }
    }
}

