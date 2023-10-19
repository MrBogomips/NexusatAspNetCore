using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Nexusat.AspNetCore.Properties;
using System;
using System.Text.Json.Serialization;

namespace Nexusat.AspNetCore.Models;

public class ApiResponseWithLocation : ApiResponse
{
    #region Helper Methods
    static IUrlHelper GetUrlHelper(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentException(nameof(context));
        }
        var urlFactory =
            context.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
        return urlFactory.GetUrlHelper(context);
    }

    static RouteValueDictionary GetRouteValueDictionary(object routeValues)
        => routeValues == null ? null : new RouteValueDictionary(routeValues);
    #endregion Helper Methods

    public class RouteDescriptor
    {
        string RouteName { get; }
        object RouteValues { get; }
        string Protocol { get; }
        string Host { get; }

        public RouteDescriptor(string routeName, object routeValues = null, string protocol = null, string host = null)
        {
            RouteName = routeName ?? throw new ArgumentNullException(nameof(routeName));
            RouteValues = RouteValues;
            Protocol = protocol;
            Host = host;
        }

        public string GetUrl(ActionContext context)
        {
            string url = GetUrlHelper(context)
                .RouteUrl(RouteName, GetRouteValueDictionary(RouteValues), Protocol, Host);
            return url ?? throw new InvalidOperationException(ExceptionMessages.NoRoutesMatched);
        }
    };

    public class ActionDescriptor
    {
        string Action { get; }
        string Controller { get; }
        object RouteValues { get; }
        bool AbsoluteUrl { get; }

        public ActionDescriptor(string actionName, string controllerName = null, object routeValues = null, bool absoluteUrl = false)
        {
            Action = actionName ?? throw new ArgumentNullException(nameof(actionName));
            Controller = controllerName;
            RouteValues = routeValues;
            AbsoluteUrl = absoluteUrl;
        }

        public static implicit operator UrlActionContext(ActionDescriptor d)
            => new UrlActionContext
            {
                Action = d.Action,
                Controller = d.Controller,
                Values = d.RouteValues
            };

        public string GetUrl(ActionContext context)
        {
            UrlActionContext ac = this;
            if (AbsoluteUrl)
            {
                ac.Protocol = context.HttpContext.Request.Scheme;
                ac.Host = context.HttpContext.Request.Host.ToUriComponent();
            }
            string url = GetUrlHelper(context).Action(ac);
            return url ?? throw new InvalidOperationException(ExceptionMessages.NoRoutesMatched);
        }
    };

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Location { get; set; }

    RouteDescriptor Route { get; }
    ActionDescriptor Action { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Models.ApiResponseWithLocation"/> class.
    /// </summary>
    /// <param name="status">Status.</param>
    /// <param name="uri">String representing an Uri</param>
    public ApiResponseWithLocation(Status status, string uri) : base(status) { 
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }         
        if(!Uri.TryCreate(uri, UriKind.RelativeOrAbsolute, out _))
        {
            throw new UriFormatException(uri);
        }
        Location = uri;
    }
    public ApiResponseWithLocation(Status status, Uri uri) : base(status)
    {
        if (uri == null)
        {
            throw new ArgumentNullException(nameof(uri));
        }
        Location = uri.ToString();
    }
    public ApiResponseWithLocation(Status status, ActionDescriptor descriptor) : base(status)
    {
        Action = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
    }
    public ApiResponseWithLocation(Status status, RouteDescriptor descriptor) : base(status)
    {
        Route = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
    }


    /// <summary>
    /// This method is called before the formatter writes to the output stream.
    /// </summary>
    public override void OnFormatting(ActionContext context)
    {
        base.OnFormatting(context);

        if (Action != null)
        {
            Location = Action.GetUrl(context);
        }
        else if (Route != null)
        {
            Location = Route.GetUrl(context);
        }

        if (Location != null)
        {
            context.HttpContext.Response.Headers[HeaderNames.Location] = Location;
        }

    }
}

public class ApiObjectResponseWithLocation<T> : ApiResponseWithLocation, IApiObjectResponse<T>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; }

    public ApiObjectResponseWithLocation(Status status, string uri, T data = default(T)) : base(status, new Uri(uri)) 
    {
        Data = data;
    }
    public ApiObjectResponseWithLocation(Status status, Uri uri, T data = default(T)) : base(status, uri)
    {
        Data = data;
    }
    public ApiObjectResponseWithLocation(Status status, ActionDescriptor descriptor, T data = default(T)) : base(status, descriptor)
    {
        Data = data;
    }
    public ApiObjectResponseWithLocation(Status status, RouteDescriptor descriptor, T data = default(T)) : base(status, descriptor)
    {
        Data = data;
    }      
}