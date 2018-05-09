using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Net.Http.Headers;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nexusat.AspNetCore.Mvc
{
	/// <summary>
	/// API controller base extensions.
	/// </summary>
	public partial class ApiController
	{
		#region Generic Response Helper Methods
		/// <summary>
		/// Produce a generic API Response without a payload
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="httpCode">Http code.</param>
		/// <param name="statusCode">Status code.</param>
		protected IApiResponse ApiResponse(int httpCode, string statusCode = null, string description = null, string userDescription = null)
		=> new ApiResponse(httpCode, statusCode, description, userDescription);

		/// <summary>
		/// Produce a generic API Response with, optionally, a payload
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="httpCode">Http code.</param>
		/// <typeparam name="T">The payload type expected by the method's signature</typeparam>
		protected IApiObjectResponse<T> ApiObjectResponse<T>(int httpCode, string statusCode = null, T data = default(T), string description = null, string userDescription = null)
		=> new ApiObjectResponse<T>(httpCode, statusCode, data, description, userDescription);

		/// <summary>
		/// Produce a generic API Response with, optionally, a payload
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="httpCode">Http code.</param>
		/// <param name="itemsCount">Number of items found</param>
		/// <typeparam name="T">The payload type expected by the method's signature</typeparam>
		protected IApiEnumResponse<T> ResponseEnum<T>(int httpCode, int itemsCount, string statusCode = null, IEnumerable<T> data = null, string description = null, string userDescription = null)
		=> new ApiEnumResponse<T>(httpCode, CurrentPage, itemsCount, data, statusCode, description, userDescription);

		/// <summary>
		/// Produce a generic API Response with, optionally, a payload
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="httpCode">Http code.</param>
		/// <param name="hasNextPage">There's another page</param>
		/// <typeparam name="T">The payload type expected by the method's signature</typeparam>
		protected IApiEnumResponse<T> ApiEnumResponse<T>(int httpCode, bool hasNextPage, string statusCode = null, IEnumerable<T> data = null, string description = null, string userDescription = null)
		=> new ApiEnumResponse<T>(httpCode, CurrentPage, hasNextPage, data, statusCode, description, userDescription);
		#endregion Generic Response Helper Methods

		#region OkResponse (HTTP 200) Helper Methods
		/// <summary>
		/// Produce an HTTP 200 response
		/// </summary>
		/// <returns>The response.</returns>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		protected IApiResponse Ok(string description = null, string userDescription = null)
		=> new ApiResponse(Status200OK, FrameworkOptions.DefaultOkStatusCode, description, userDescription);

		/// <summary>
		/// Produce an HTTP 200 response 
		/// </summary>
		/// <returns>The object response.</returns>
		/// <param name="data">Data.</param>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected IApiObjectResponse<T> OkObject<T>(T data = default(T), string description = null, string userDescription = null)
		=> new ApiObjectResponse<T>(Status200OK, FrameworkOptions.DefaultOkStatusCode, data, description, userDescription);
		/// <summary>
		/// Produce an HTTP 200 response
		/// </summary>
		/// <returns>The enum response.</returns>
		/// <param name="itemsCount">The count of items found.</param>
		/// <param name="data">Data.</param>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected IApiEnumResponse<T> OkEnum<T>(int itemsCount, IEnumerable<T> data = null, string description = null, string userDescription = null)
		=> new ApiEnumResponse<T>(Status200OK, CurrentPage, itemsCount, data, FrameworkOptions.DefaultOkStatusCode, description, userDescription);
		/// <summary>
		/// Produce an HTTP 200 response
		/// </summary>
		/// <returns>The enum response.</returns>
		/// <param name="hasNextPage">There's another page</param>
		/// <param name="data">Data.</param>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected IApiEnumResponse<T> OkEnum<T>(bool hasNextPage, IEnumerable<T> data = null, string description = null, string userDescription = null)
		=> new ApiEnumResponse<T>(Status200OK, CurrentPage, hasNextPage, data, FrameworkOptions.DefaultOkStatusCode, description, userDescription);
        /// <summary>
        /// Produce an empty response.
		/// Suitable in case of no data available.
        /// </summary>
        /// <returns>The empty enum.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
		protected IApiEnumResponse<T> OkEmptyEnum<T>() => throw new NoContentResponseException();
		#endregion OkResponse (HTTP 200) Helper Methods

		#region AcceptedResponse (HTTP 202) Helper Methods
		/// <summary>
		/// Produce an HTTP 202 response
		/// </summary>
		/// <returns>The accepted.</returns>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <param name="uri">URI.</param>
		protected IApiResponse Accepted(string description = null, string userDescription = null, string uri = null)
		=> ApiResponse(Status202Accepted, FrameworkOptions.DefaultOkStatusCode, description, userDescription)
			.SetLocation(uri);

		/// <summary>
		/// Produce an HTTP 202 response
		/// </summary>
		/// <returns>The accepted.</returns>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <param name="uri">URI.</param>
		protected IApiResponse Accepted(string description = null, string userDescription = null, Uri uri = null)
		=> Accepted(description, userDescription, uri.ToString());
		/// <summary>
		/// Produce an HTTP 202 response
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="data">Data.</param>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <param name="uri">URI.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected IApiObjectResponse<T> Accepted<T>(T data = default(T), string description = null, string userDescription = null, string uri = null)
		=> ApiObjectResponse<T>(Status202Accepted, FrameworkOptions.DefaultOkStatusCode, data, description, userDescription)
			.SetLocation(uri);
		/// <summary>
		/// Produce an HTTP 202 response
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="data">Data.</param>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <param name="uri">URI.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected IApiObjectResponse<T> Accepted<T>(T data = default(T), string description = null, string userDescription = null, Uri uri = null)
		=> Accepted(data, description, userDescription, uri.ToString());

		protected IApiResponse AcceptedAtAction(string actionName, string controllerName, object routeValues, string description = null, string userDescription = null)
		=> Accepted(description, userDescription, GetActionUrl(actionName, controllerName, routeValues));
		protected IApiResponse AcceptedAtAction(string actionName, object routeValues, string description = null, string userDescription = null)
		=> Accepted(description, userDescription, GetActionUrl(actionName, routeValues: routeValues));
		protected IApiResponse AcceptedAtAction(string actionName, string description = null, string userDescription = null)
		=> Accepted(description, userDescription, GetActionUrl(actionName));

		protected IApiObjectResponse<T> AcceptedAtAction<T>(string actionName, string controllerName, object routeValues, T data = default(T), string description = null, string userDescription = null)
		=> Accepted(data, description, userDescription, GetActionUrl(actionName, controllerName, routeValues));
		protected IApiObjectResponse<T> AcceptedAtAction<T>(string actionName, object routeValues, T data = default(T), string description = null, string userDescription = null)
		=> Accepted(data, description, userDescription, GetActionUrl(actionName, routeValues: routeValues));
		protected IApiObjectResponse<T> AcceptedAtAction<T>(string actionName, T data = default(T), string description = null, string userDescription = null)
		=> Accepted(data, description, userDescription, GetActionUrl(actionName));

		protected IApiResponse AcceptedAtRoute(string routeName, object routeValues, string description = null, string userDescription = null)
		=> Accepted(description, userDescription, GetLink(routeName, routeValues));
		protected IApiResponse AcceptedAtRoute(string routeName, string description = null, string userDescription = null)
		=> Accepted(description, userDescription, GetLink(routeName));

		protected IApiObjectResponse<T> AcceptedAtRoute<T>(string routeName, object routeValues, T data = default(T), string description = null, string userDescription = null)
		=> Accepted(data, description, userDescription, GetLink(routeName, routeValues));
		protected IApiObjectResponse<T> AcceptedAtRoute<T>(string routeName, T data = default(T), string description = null, string userDescription = null)
		=> Accepted(data, description, userDescription, GetLink(routeName));

		#endregion AcceptedResponse (HTTP 202) Helper Methods

		#region CreatedResponse (HTTP 201) Helper Methods
		/// <summary>
		/// Produce an HTTP 202 response
		/// </summary>
		/// <returns>The accepted.</returns>
		/// <param name="description">Description.</param>
		/// <param name="userDescription">User description.</param>
		/// <param name="uri">URI.</param>
		protected IApiResponse Created(string description = null, string userDescription = null, string uri = null)
		=> ApiResponse(Status201Created, FrameworkOptions.DefaultOkStatusCode, description, userDescription)
			.SetLocation(uri);
		protected IApiResponse Created(string description = null, string userDescription = null, Uri uri = null)
		=> Created(description, userDescription, uri.ToString());
		protected IApiObjectResponse<T> Created<T>(T data = default(T), string description = null, string userDescription = null, string uri = null)
		=> ApiObjectResponse(Status201Created, FrameworkOptions.DefaultOkStatusCode, data, description, userDescription)
			.SetLocation(uri);
		protected IApiObjectResponse<T> Created<T>(T data = default(T), string description = null, string userDescription = null, Uri uri = null)
		=> Created(data, description, userDescription, uri.ToString());

		protected IApiResponse CreatedAtAction(string actionName, string controllerName, object routeValues, string description = null, string userDescription = null)
		=> Created(description, userDescription, GetActionUrl(actionName, controllerName, routeValues));
		protected IApiResponse CreatedAtAction(string actionName, object routeValues, string description = null, string userDescription = null)
		=> Created(description, userDescription, GetActionUrl(actionName, routeValues: routeValues));
		protected IApiResponse CreatedAtAction(string actionName, string description = null, string userDescription = null)
		=> Created(description, userDescription, GetActionUrl(actionName));

		protected IApiObjectResponse<T> CreatedAtAction<T>(string actionName, string controllerName, object routeValues, T data = default(T), string description = null, string userDescription = null)
		=> Created(data, description, userDescription, GetActionUrl(actionName, controllerName, routeValues));
		protected IApiObjectResponse<T> CreatedAtAction<T>(string actionName, object routeValues, T data = default(T), string description = null, string userDescription = null)
		=> Created(data, description, userDescription, GetActionUrl(actionName, routeValues: routeValues));
		protected IApiObjectResponse<T> CreatedAtAction<T>(string actionName, T data = default(T), string description = null, string userDescription = null)
		=> Created(data, description, userDescription, GetActionUrl(actionName));

		protected IApiResponse CreatedAtRoute(string routeName, object routeValues, string description = null, string userDescription = null)
		=> Created(description, userDescription, GetLink(routeName, routeValues));
		protected IApiResponse CreatedAtRoute(string routeName, string description = null, string userDescription = null)
		=> Created(description, userDescription, GetLink(routeName));

		protected IApiObjectResponse<T> CreatedAtRoute<T>(string routeName, object routeValues, T data = default(T), string description = null, string userDescription = null)
		=> Created(data, description, userDescription, GetLink(routeName, routeValues));
		protected IApiObjectResponse<T> CreatedAtRoute<T>(string routeName, T data = default(T), string description = null, string userDescription = null)
		=> Created(data, description, userDescription, GetLink(routeName));
		#endregion CreatedResponse (HTTP 201) Helper Methods

		#region BadRequest (HTTP 400) Helper Methods
		protected IApiResponse BadRequest(string statusCode, string description = null, string userDescription = null)
		=> new BadRequestResponse(statusCode, description, userDescription);
		protected IApiResponse BadRequest(string description = null, string userDescription = null)
        => new BadRequestResponse(description, userDescription);
		protected IApiResponse BadRequest(ModelStateDictionary modelState)
		=> new BadRequestResponse(ModelState);

		protected IApiObjectResponse<T> BadObjectRequest<T>(string statusCode, T data = default(T), string description = null, string userDescription = null)
		=> new BadObjectRequestResponse<T>(statusCode, data, description, userDescription);
		protected IApiObjectResponse<T> BadObjectRequest<T>(T data = default(T), string description = null, string userDescription = null)
		=> new BadObjectRequestResponse<T>(description, data, userDescription);
		protected IApiObjectResponse<T> BadObjectRequest<T>(ModelStateDictionary modelState, T data = default(T))
		=> new BadObjectRequestResponse<T>(modelState, data);      
		#endregion BadRequest (HTTP 400) Helper Methods

#if TO_BE_IMPLEMENTED
        // Please refer to Microsoft.AspNetCore.Mvc.BaseController
        // See for referece: https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs


        //
        // Methods
        //
        
        [NonAction]
        public virtual BadRequestObjectResult BadRequest (ModelStateDictionary modelState);

        [NonAction]
        public virtual BadRequestObjectResult BadRequest (object error);

        [NonAction]
        public virtual BadRequestResult BadRequest ();

        [NonAction]
        public virtual ChallengeResult Challenge ();

        [NonAction]
        public virtual ChallengeResult Challenge (params string[] authenticationSchemes);

        [NonAction]
        public virtual ChallengeResult Challenge (AuthenticationProperties properties);

        [NonAction]
        public virtual ChallengeResult Challenge (AuthenticationProperties properties, params string[] authenticationSchemes);

        [NonAction]
        public virtual FileContentResult File (byte[] fileContents, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual FileStreamResult File (Stream fileStream, string contentType);

        [NonAction]
        public virtual FileStreamResult File (Stream fileStream, string contentType, string fileDownloadName);

        [NonAction]
        public virtual VirtualFileResult File (string virtualPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual FileStreamResult File (Stream fileStream, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual VirtualFileResult File (string virtualPath, string contentType, string fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual FileContentResult File (byte[] fileContents, string contentType, string fileDownloadName);

        [NonAction]
        public virtual FileContentResult File (byte[] fileContents, string contentType);

        [NonAction]
        public virtual VirtualFileResult File (string virtualPath, string contentType);

        [NonAction]
        public virtual FileContentResult File (byte[] fileContents, string contentType, string fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual VirtualFileResult File (string virtualPath, string contentType, string fileDownloadName);

        [NonAction]
        public virtual FileStreamResult File (Stream fileStream, string contentType, string fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual ForbidResult Forbid ();

        [NonAction]
        public virtual ForbidResult Forbid (params string[] authenticationSchemes);

        [NonAction]
        public virtual ForbidResult Forbid (AuthenticationProperties properties);

        [NonAction]
        public virtual ForbidResult Forbid (AuthenticationProperties properties, params string[] authenticationSchemes);

        [NonAction]
        public virtual LocalRedirectResult LocalRedirect (string localUrl);

        [NonAction]
        public virtual LocalRedirectResult LocalRedirectPermanent (string localUrl);

        [NonAction]
        public virtual LocalRedirectResult LocalRedirectPermanentPreserveMethod (string localUrl);

        [NonAction]
        public virtual LocalRedirectResult LocalRedirectPreserveMethod (string localUrl);

        [NonAction]
        public virtual NoContentResult NoContent ();

        [NonAction]
        public virtual NotFoundObjectResult NotFound (object value);

        [NonAction]
        public virtual NotFoundResult NotFound ();

        [NonAction]
        public virtual PhysicalFileResult PhysicalFile (string physicalPath, string contentType, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual PhysicalFileResult PhysicalFile (string physicalPath, string contentType, string fileDownloadName);

        [NonAction]
        public virtual PhysicalFileResult PhysicalFile (string physicalPath, string contentType);

        [NonAction]
        public virtual PhysicalFileResult PhysicalFile (string physicalPath, string contentType, string fileDownloadName, DateTimeOffset? lastModified, EntityTagHeaderValue entityTag);

        [NonAction]
        public virtual RedirectResult Redirect (string url);

        [NonAction]
        public virtual RedirectResult RedirectPermanent (string url);

        [NonAction]
        public virtual RedirectResult RedirectPermanentPreserveMethod (string url);

        [NonAction]
        public virtual RedirectResult RedirectPreserveMethod (string url);

        [NonAction]
        public virtual RedirectToActionResult RedirectToAction (string actionName);

        [NonAction]
        public virtual RedirectToActionResult RedirectToAction (string actionName, object routeValues);

        [NonAction]
        public virtual RedirectToActionResult RedirectToAction (string actionName, string controllerName);

        [NonAction]
        public virtual RedirectToActionResult RedirectToAction (string actionName, string controllerName, object routeValues);

        [NonAction]
        public virtual RedirectToActionResult RedirectToAction (string actionName, string controllerName, string fragment);

        [NonAction]
        public virtual RedirectToActionResult RedirectToAction (string actionName, string controllerName, object routeValues, string fragment);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanent (string actionName, string controllerName, object routeValues);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanent (string actionName, string controllerName, string fragment);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanent (string actionName);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanent (string actionName, string controllerName, object routeValues, string fragment);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanent (string actionName, object routeValues);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanent (string actionName, string controllerName);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPermanentPreserveMethod (string actionName = null, string controllerName = null, object routeValues = null, string fragment = null);

        [NonAction]
        public virtual RedirectToActionResult RedirectToActionPreserveMethod (string actionName = null, string controllerName = null, object routeValues = null, string fragment = null);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPage (string pageName, string pageHandler, string fragment);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPage (string pageName);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPage (string pageName, object routeValues);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPage (string pageName, string pageHandler);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPage (string pageName, string pageHandler, object routeValues);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPage (string pageName, string pageHandler, object routeValues, string fragment);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePermanent (string pageName, string pageHandler, object routeValues, string fragment);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePermanent (string pageName, string pageHandler, string fragment);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePermanent (string pageName, string pageHandler);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePermanent (string pageName, object routeValues);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePermanent (string pageName);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePermanentPreserveMethod (string pageName, string pageHandler = null, object routeValues = null, string fragment = null);

        [NonAction]
        public virtual RedirectToPageResult RedirectToPagePreserveMethod (string pageName, string pageHandler = null, object routeValues = null, string fragment = null);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoute (string routeName);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoute (object routeValues);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoute (string routeName, object routeValues, string fragment);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoute (string routeName, string fragment);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoute (string routeName, object routeValues);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePermanent (string routeName);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePermanent (string routeName, object routeValues, string fragment);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePermanent (string routeName, string fragment);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePermanent (string routeName, object routeValues);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePermanent (object routeValues);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePermanentPreserveMethod (string routeName = null, object routeValues = null, string fragment = null);

        [NonAction]
        public virtual RedirectToRouteResult RedirectToRoutePreserveMethod (string routeName = null, object routeValues = null, string fragment = null);

        [NonAction]
        public virtual SignInResult SignIn (ClaimsPrincipal principal, string authenticationScheme);

        [NonAction]
        public virtual SignInResult SignIn (ClaimsPrincipal principal, AuthenticationProperties properties, string authenticationScheme);

        [NonAction]
        public virtual SignOutResult SignOut (params string[] authenticationSchemes);

        [NonAction]
        public virtual SignOutResult SignOut (AuthenticationProperties properties, params string[] authenticationSchemes);

        [NonAction]
        public virtual StatusCodeResult StatusCode (int statusCode);

        [NonAction]
        public virtual ObjectResult StatusCode (int statusCode, object value);

        [NonAction]
        public virtual Task<bool> TryUpdateModelAsync<TModel> (TModel model) where TModel : class;

        [NonAction, AsyncStateMachine (typeof(ControllerBase.<TryUpdateModelAsync>d__148<>))]
        public virtual Task<bool> TryUpdateModelAsync<TModel> (TModel model, string prefix) where TModel : class;

        [NonAction]
        public virtual Task<bool> TryUpdateModelAsync<TModel> (TModel model, string prefix, IValueProvider valueProvider) where TModel : class;

        [NonAction, AsyncStateMachine (typeof(ControllerBase.<TryUpdateModelAsync>d__150<>))]
        public Task<bool> TryUpdateModelAsync<TModel> (TModel model, string prefix, params Expression<Func<TModel, object>>[] includeExpressions) where TModel : class;

        [NonAction, AsyncStateMachine (typeof(ControllerBase.<TryUpdateModelAsync>d__151<>))]
        public Task<bool> TryUpdateModelAsync<TModel> (TModel model, string prefix, Func<ModelMetadata, bool> propertyFilter) where TModel : class;

        [NonAction]
        public Task<bool> TryUpdateModelAsync<TModel> (TModel model, string prefix, IValueProvider valueProvider, params Expression<Func<TModel, object>>[] includeExpressions) where TModel : class;

        [NonAction]
        public Task<bool> TryUpdateModelAsync<TModel> (TModel model, string prefix, IValueProvider valueProvider, Func<ModelMetadata, bool> propertyFilter) where TModel : class;

        [NonAction, AsyncStateMachine (typeof(ControllerBase.<TryUpdateModelAsync>d__154))]
        public virtual Task<bool> TryUpdateModelAsync (object model, Type modelType, string prefix);

        [NonAction]
        public Task<bool> TryUpdateModelAsync (object model, Type modelType, string prefix, IValueProvider valueProvider, Func<ModelMetadata, bool> propertyFilter);

        [NonAction]
        public virtual bool TryValidateModel (object model, string prefix);

        [NonAction]
        public virtual bool TryValidateModel (object model);

        [NonAction]
        public virtual UnauthorizedResult Unauthorized ();
#endif // TO_BE_IMPLEMENTD
	}
}
