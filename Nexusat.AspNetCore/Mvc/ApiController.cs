using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

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
        private IUrlHelper _UrlHelper;

        [ControllerContext]
        public ControllerContext ControllerContext { get; set; }

        protected HttpContext HttpContext { get => ControllerContext.HttpContext; }
        protected ModelStateDictionary ModelState { get => ControllerContext.ModelState; }
        protected RouteData RoutedData { get => ControllerContext.RouteData; }
        protected IUrlHelper UrlHelper { 
            get {
                if (_UrlHelper != null)
                {
                    return _UrlHelper;
                }
                var urlFactory =
                    ControllerContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
                _UrlHelper = urlFactory.GetUrlHelper(ControllerContext);
                return _UrlHelper;
            } set {
                _UrlHelper = value;
            }
        }

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
        /// <summary>
        /// Helper method to get the action Url.
        /// 
        /// <see cref="IUrlHelper.Action(UrlActionContext)"/>
        /// </summary>
        /// <returns>The action URL.</returns>
        /// <param name="actionName">Action name.</param>
        /// <param name="controllerName">Controller name.</param>
        /// <param name="routeValues">Route values.</param>
        /// <param name="fragment">Fragment.</param>
        /// <param name="absoluteUrl">If <c>true</c> then also protocol and host part is added</param>
        protected string GetActionUrl(string actionName, string controllerName = null, object routeValues = null, string fragment = null, bool absoluteUrl = false)
        {
            if (actionName == null)
            {
                throw new ArgumentNullException(nameof(actionName));
            }
            var _routeValues = routeValues == null ? null : new RouteValueDictionary(routeValues);

            var ac = new UrlActionContext
            {
                Action = actionName,
                Controller = controllerName,
                Values = _routeValues,
                Fragment = fragment
            };

            if (absoluteUrl) {
                ac.Protocol = HttpContext.Request.Scheme;
                ac.Host = HttpContext.Request.Host.ToUriComponent();
            }

            var url = UrlHelper.Action(ac);

            if (url == null)
            {
                throw new InvalidOperationException(ExceptionMessages.NoRoutesMatched);
            }

            return url;
        }
        /// <summary>
        /// Gets a URL provided the route info.
        /// 
        /// <see cref="IUrlHelper.RouteUrl(UrlRouteContext)"/>
        /// </summary>
        /// <returns>The link.</returns>
        /// <param name="routeName">Route name.</param>
        /// <param name="routeValues">Route values.</param>
        /// <param name="protocol">Protocol.</param>
        /// <param name="host">Host.</param>
        /// <param name="fragment">Fragment.</param>
        protected string GetLink(string routeName, object routeValues = null, string protocol = null, string host = null, string fragment = null) {
            if (routeName == null)
            {
                throw new ArgumentNullException(nameof(routeName));
            }
            var _routeValues = routeValues == null ? null : new RouteValueDictionary(routeValues);

            var url = UrlHelper.RouteUrl(routeName, _routeValues, protocol, host, fragment);

            if (url == null)
            {
                throw new InvalidOperationException(ExceptionMessages.NoRoutesMatched);
            }

            return url;
        }
    }
}
