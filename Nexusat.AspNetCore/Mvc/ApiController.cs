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

namespace Nexusat.AspNetCore.Mvc
{
    [Controller]
    public abstract partial class ApiController
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

        protected NexusatAspNetCoreOptions FrameworkOptions
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

        #region General purpose Response builder methods
        protected IApiResponse ApiResponse(Action<IApiResponseBuilder> setupResponseAction) {
            IApiResponseBuilder builder = ResponseBuilderFactory.GetApiResponseBuilder();
            setupResponseAction(builder);
            IApiResponse response = builder.GetResponse();
            return response;
        }

        protected IApiObjectResponse<T> ApiObjectResponse<T>(Action<IApiObjectResponseBuilder<T>> setupResponseAction)
        {
            IApiObjectResponseBuilder<T> builder = ResponseBuilderFactory.GetApiObjectResponseBuilder<T>();
            setupResponseAction(builder);
            IApiObjectResponse<T> response = builder.GetResponse();
            return response;
        }

        protected IApiEnumResponse<T> ApiEnumResponse<T>(Action<IApiEnumResponseBuilder<T>> setupResponseAction)
        {
            IApiEnumResponseBuilder<T> builder = ResponseBuilderFactory.GetApiEnumResponseBuilder<T>();
            setupResponseAction(builder);
            IApiEnumResponse<T> response = builder.GetResponse();
            return response;
        }
        #endregion General purpose Response builder methods




    }
}
