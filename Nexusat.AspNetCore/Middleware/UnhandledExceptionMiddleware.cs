using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc.Formatters;
using static Nexusat.AspNetCore.Utils.StringFormatter;

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
                ApiResponse response = null;
                if (exception is IApiResponseException)
                {
                    switch (exception)
                    {
                        case BadRequestResponseException br:
                            response = BadRequest.ApiResponse.GetFromException(br);
                            break;
                        case NoContentResponseException nr:
                            response = new NoContentResponse();
                            break;
                        default: // just in case of something missed
                            logger.LogWarning(FormatSystemMessage("An Api Response Exception {0}({1}) was found", exception.GetType().FullName, exception.Message));
                            IApiResponseException apiException = exception as IApiResponseException;
                            response = new ApiResponse(apiException.HttpCode, apiException.StatusCode, apiException.Description, apiException.UserDescription);
                            response.HasBody = apiException.HasBody;
                            break;
                    }
                } else {
                    response = new UnhandledExceptionResponse(exception);
                }

                executor.RenderResponse(context, response);
            } else {
                logger.LogCritical("Response already started. Unable to generate a response");
            }

            return Task.FromResult(0);
        }
    }
}

