using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Nexusat.AspNetCore.Builders;
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

        protected IApiResponseBuilderFactory ResponseBuilderFactory {
            get => HttpContext.RequestServices.GetService(typeof(IApiResponseBuilderFactory)) as IApiResponseBuilderFactory;
        }

        private void SetHttpStatusCode(int httpCode, IApiResponseBuilder responseBuilder)
        {
            HttpContext.Response.StatusCode = httpCode;
            responseBuilder.SetHttpCode(httpCode);
            if (httpCode == 200)
            {
                responseBuilder.SetStatusCodeSuccess("HTTP_200");
            }
            else if (100 <= httpCode && httpCode < 200)
            {
                responseBuilder.SetStatusCodeSuccess("HTTP_1xx");
            }

        }

        protected IApiResponse Response(int httpCode)
        {
            HttpContext.Response.StatusCode = httpCode;
            return ResponseBuilderFactory
                .GetApiResponseBuilder()
                .SetHttpCode(httpCode)
                .Build();
        }
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

        protected IApiResponse OkResponse() => Response(Status200OK);
        protected IApiObjectResponse<T> OkObjectResponse<T>() => ObjectResponse<T>(Status200OK);
        protected IApiEnumResponse<T> OkEnumResponse<T>() => EnumResponse<T>(Status200OK);

    }
}
