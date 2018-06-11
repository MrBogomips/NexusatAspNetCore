using System;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Models
{

	/*
     * protected IApiResponse Created(string description = null, string userDescription = null, string uri = null)
        => ApiResponse(Status201Created, FrameworkOptions.DefaultOkStatusCode, description, userDescription)
            .SetLocation(uri);
        protected IApiResponse Created(string description = null, string userDescription = null, Uri uri = null)
        => Created(description, userDescription, uri.ToString());
        protected IApiObjectResponse<T> Created<T>(T data = default(T), string description = null, string userDescription = null, string uri = null)
        => ApiObjectResponse(Status201Created, FrameworkOptions.DefaultOkStatusCode, data, description, userDescription)
            .SetLocation(uri);
        protected IApiObjectResponse<T> Created<T>(T data = default(T), string description = null, string userDescription = null, Uri uri = null)
        => Created(data, description, userDescription, uri.ToString());
     */


	public static class Created
    {
		public const string ApiStatusCode = CommonStatusCodes.OK_ + "CREATED";
		public const int HttpStatusCode = StatusCodes.Status201Created;

		public class ResponseAtUri : ApiResponseWithLocation
        {
            public ResponseAtUri(string uri, string description = null, string userDescription = null)
                :base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), uri) 
            { }
            public ResponseAtUri(Uri uri, string description = null, string userDescription = null)
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), uri)
            { }         
            public ResponseAtUri(string statusCode, string uri, string description = null, string userDescription = null)
                :base(new Status(HttpStatusCode, statusCode, description, userDescription), uri) 
            {
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
            public ResponseAtUri(string statusCode, Uri uri, string description = null, string userDescription = null)
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), uri) 
            { 
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
        }
        
        public class ResponseAtAction: ApiResponseWithLocation
        {
            public ResponseAtAction(string actionName, string description = null, string userDescription = null, string controllerName = null, object routeValues = null, bool absoluteUrl = false) 
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), new ActionDescriptor(actionName, controllerName, routeValues, absoluteUrl)) { }

            public ResponseAtAction(string statusCode, string actionName, string description = null, string userDescription = null, string controllerName = null, object routeValues = null, bool absoluteUrl = false) 
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), new ActionDescriptor(actionName, controllerName, routeValues, absoluteUrl)) 
            {
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
        }

        public class ResponseAtRoute: ApiResponseWithLocation
        {
            public ResponseAtRoute(string routeName, string description = null, string userDescription = null, object routeValues = null, string protocol = null, string host = null) 
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), new RouteDescriptor(routeName, routeValues, protocol, host)) { }

            public ResponseAtRoute(string statusCode, string routeName, string description = null, string userDescription = null, object routeValues = null, string protocol = null, string host = null)
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), new RouteDescriptor(routeName, routeValues, protocol, host)) 
            { 
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
        } 
        
		public class ObjectAtUri<T>: ApiObjectResponseWithLocation<T>
		{
			public ObjectAtUri(string uri, T data = default(T), string description = null, string userDescription = null)
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), uri, data)
            { }
			public ObjectAtUri(Uri uri, T data = default(T), string description = null, string userDescription = null)
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), uri, data)
            { }
			public ObjectAtUri(string statusCode, string uri, T data = default(T), string description = null, string userDescription = null)
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), uri, data)
            {
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
			public ObjectAtUri(string statusCode, Uri uri, T data = default(T), string description = null, string userDescription = null)
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), uri, data)
            {
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
		}

		public class ObjectAtAction<T>: ApiObjectResponseWithLocation<T>
		{
			public ObjectAtAction(string actionName, T data = default(T), string description = null, string userDescription = null, string controllerName = null, object routeValues = null, bool absoluteUrl = false)
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), new ActionDescriptor(actionName, controllerName, routeValues, absoluteUrl), data) { }

			public ObjectAtAction(string statusCode, string actionName, T data = default(T), string description = null, string userDescription = null, string controllerName = null, object routeValues = null, bool absoluteUrl = false)
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), new ActionDescriptor(actionName, controllerName, routeValues, absoluteUrl), data)
            {
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
		}

		public class ObjectAtRoute<T>: ApiObjectResponseWithLocation<T>
		{
			public ObjectAtRoute(string routeName, T data = default(T), string description = null, string userDescription = null, object routeValues = null, string protocol = null, string host = null)
                : base(new Status(HttpStatusCode, ApiStatusCode, description, userDescription), new RouteDescriptor(routeName, routeValues, protocol, host), data) { }

			public ObjectAtRoute(string statusCode, string routeName, T data = default(T), string description = null, string userDescription = null, object routeValues = null, string protocol = null, string host = null)
                : base(new Status(HttpStatusCode, statusCode, description, userDescription), new RouteDescriptor(routeName, routeValues, protocol, host), data)
            {
                StatusCode.CheckValidOkCodeOrThrow(statusCode);
            }
		}
	}
}
