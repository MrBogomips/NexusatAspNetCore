using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Models;

using System;
using System.Collections.Generic;
using System.Text;

using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Mvc
{
    [Controller]
    public abstract class ApiController
    {
        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }

        protected HttpContext HttpContext { get => ControllerContext.HttpContext; }
        protected ModelStateDictionary ModelState { get => ControllerContext.ModelState; }
        protected RouteData RoutedData { get => ControllerContext.RouteData; }

        protected IApiResponseBuilderFactory ResponseBuilderFactory
        {
            get => HttpContext.RequestServices.GetService(typeof(IApiResponseBuilderFactory)) as IApiResponseBuilderFactory;
        }

        protected internal NexusatAspNetCoreOptions FrameworkOptions
        {
            get
            {
                var options = HttpContext
                    .RequestServices
                    .GetService(typeof(IOptions<NexusatAspNetCoreOptions>))
                    as IOptions<NexusatAspNetCoreOptions>;
                return options.Value;
            }
        }

        private void SetHttpStatusCode(int httpCode, IApiResponseBuilder responseBuilder)
        {
            HttpContext.Response.StatusCode = httpCode;
            responseBuilder.SetHttpCode(httpCode);
        }

        /// <summary>
        /// Produce a generic API Response without a payload
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="httpCode">Http code.</param>
        /// <param name="statusCode">Status code.</param>
        protected IApiResponse Response(int httpCode, string statusCode)
        {
            HttpContext.Response.StatusCode = httpCode;
            return ResponseBuilderFactory
                .GetApiResponseBuilder()
                .SetHttpCode(httpCode)
                .SetStatusCode(statusCode)
                .Build();
        }
        /// <summary>
        /// Produce a generic API Response without a payload
        /// </summary>
        /// <returns>The response.</returns>
        /// <param name="httpCode">Http code.</param>
        /// <typeparam name="T">The payload type expected by the method's signature</typeparam>
        protected IApiObjectResponse<T> ObjectResponse<T>(int httpCode)
        {
            HttpContext.Response.StatusCode = httpCode;
            return ResponseBuilderFactory
                .GetApiObjectResponseBuilder<T>()
                .SetHttpCode(httpCode)
                .Build();
        }
        protected IApiEnumResponse<T> EnumResponse<T>(int httpCode)
        {
            HttpContext.Response.StatusCode = httpCode;
            return ResponseBuilderFactory
                .GetApiEnumResponseBuilder<T>()
                .SetHttpCode(httpCode)
                .Build();
        }


        #region Response Helper Methods
        // Ok 200
        protected IApiResponse OkResponse() => Response(Status200OK, FrameworkOptions.DefaultOkStatusCode);
        protected IApiObjectResponse<T> OkObjectResponse<T>() => ObjectResponse<T>(Status200OK);
        protected IApiEnumResponse<T> OkEnumResponse<T>() => EnumResponse<T>(Status200OK);

        #endregion Response Helper Methods

    }
}
