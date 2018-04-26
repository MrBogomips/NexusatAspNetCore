using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

using Nexusat.AspNetCore.Configuration;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Properties;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Mvc
{
    /// <summary>
    /// The base class from which any API controller should derive from.
    /// </summary>
    [Controller]
    public abstract partial class ApiController
    {
        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }

        protected HttpContext HttpContext { get => ControllerContext.HttpContext; }
        protected ModelStateDictionary ModelState { get => ControllerContext.ModelState; }
        protected RouteData RoutedData { get => ControllerContext.RouteData; }
        /// <summary>
        /// Gets the current page or throws an <see cref="InvalidOperationException"/>
        /// if you missed to decorate your actio method with <see cref=" ValidatePaginationAttribute"/>.
        /// </summary>
        /// <value>The current page.</value>
        protected PaginationCursor CurrentPage
        {
            get =>
            HttpContext.Items[InternalConstants.PaginationCursorKey] as PaginationCursor 
                       ?? throw new InvalidOperationException(FormatSystemMessage(ExceptionMessages.InvalidStatePaginationCursor));
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
    }
}
