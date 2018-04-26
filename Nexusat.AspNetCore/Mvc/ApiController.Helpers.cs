using System;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

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
        => new ApiEnumResponse<T>(httpCode, CurrentPage, itemsCount, data, statusCode, description, userDescription );

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
        {
            if (uri != null)
                HttpContext.Response.Headers[HeaderNames.Location] = uri;
            return ApiResponse(Status202Accepted,
                    FrameworkOptions.DefaultOkStatusCode,
                    description: description,
                    userDescription: userDescription);
        }
        /// <summary>
        /// Produce an HTTP 202 response
        /// </summary>
        /// <returns>The accepted.</returns>
        /// <param name="description">Description.</param>
        /// <param name="userDescription">User description.</param>
        /// <param name="uri">URI.</param>
        protected IApiResponse Accepted(string description = null, string userDescription = null, Uri uri = null) => Accepted(description, userDescription, uri.ToString());
        /// <summary>
        /// Produce an HTTP 202 response
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="data">Data.</param>
        /// <param name="description">Description.</param>
        /// <param name="userDescription">User description.</param>
        /// <param name="uri">URI.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected IApiObjectResponse<T> AcceptedObject<T>(T data = default(T), string description = null, string userDescription = null, string uri = null)
        {
            if (uri != null)
                HttpContext.Response.Headers[HeaderNames.Location] = uri;
            return ApiObjectResponse<T>(Status202Accepted,
                    FrameworkOptions.DefaultOkStatusCode,
                    description: description,
                    userDescription: userDescription,
                    data: data);
        }
        /// <summary>
        /// Produce an HTTP 202 response
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="data">Data.</param>
        /// <param name="description">Description.</param>
        /// <param name="userDescription">User description.</param>
        /// <param name="uri">URI.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected IApiObjectResponse<T> AcceptedObject<T>(T data = default(T), string description = null, string userDescription = null, Uri uri = null)
        => AcceptedObject(data, description, userDescription, uri.ToString());
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
        {
            if (uri != null)
                HttpContext.Response.Headers[HeaderNames.Location] = uri;
            return ApiResponse(Status201Created,
                    FrameworkOptions.DefaultOkStatusCode,
                    description: description,
                    userDescription: userDescription);
        }
        #endregion CreatedResponse (HTTP 201) Helper Methods

#if TO_BE_IMPLEMENTED
        // Please refer to Microsoft.AspNetCore.Mvc.BaseController
        // See for referece: https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs


        //
        // Methods
        //
        [NonAction]
        public virtual AcceptedResult Accepted ();

        [NonAction]
        public virtual AcceptedResult Accepted (object value);

        [NonAction]
        public virtual AcceptedResult Accepted (Uri uri);

        [NonAction]
        public virtual AcceptedResult Accepted (Uri uri, object value);

        [NonAction]
        public virtual AcceptedResult Accepted (string uri, object value);

        [NonAction]
        public virtual AcceptedResult Accepted (string uri);

        [NonAction]
        public virtual AcceptedAtActionResult AcceptedAtAction (string actionName, object value);

        [NonAction]
        public virtual AcceptedAtActionResult AcceptedAtAction (string actionName, string controllerName, object routeValues, object value);

        [NonAction]
        public virtual AcceptedAtActionResult AcceptedAtAction (string actionName, object routeValues, object value);

        [NonAction]
        public virtual AcceptedAtActionResult AcceptedAtAction (string actionName, string controllerName, object routeValues);

        [NonAction]
        public virtual AcceptedAtActionResult AcceptedAtAction (string actionName, string controllerName);

        [NonAction]
        public virtual AcceptedAtActionResult AcceptedAtAction (string actionName);

        [NonAction]
        public virtual AcceptedAtRouteResult AcceptedAtRoute (string routeName);

        [NonAction]
        public virtual AcceptedAtRouteResult AcceptedAtRoute (string routeName, object routeValues);

        [NonAction]
        public virtual AcceptedAtRouteResult AcceptedAtRoute (object routeValues, object value);

        [NonAction]
        public virtual AcceptedAtRouteResult AcceptedAtRoute (string routeName, object routeValues, object value);

        [NonAction]
        public virtual AcceptedAtRouteResult AcceptedAtRoute (object routeValues);

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
        public virtual ContentResult Content (string content);

        [NonAction]
        public virtual ContentResult Content (string content, string contentType);

        [NonAction]
        public virtual ContentResult Content (string content, MediaTypeHeaderValue contentType);

        [NonAction]
        public virtual ContentResult Content (string content, string contentType, Encoding contentEncoding);

        [NonAction]
        public virtual CreatedResult Created (Uri uri, object value);

        [NonAction]
        public virtual CreatedResult Created (string uri, object value);

        [NonAction]
        public virtual CreatedAtActionResult CreatedAtAction (string actionName, string controllerName, object routeValues, object value);

        [NonAction]
        public virtual CreatedAtActionResult CreatedAtAction (string actionName, object routeValues, object value);

        [NonAction]
        public virtual CreatedAtActionResult CreatedAtAction (string actionName, object value);

        [NonAction]
        public virtual CreatedAtRouteResult CreatedAtRoute (string routeName, object value);

        [NonAction]
        public virtual CreatedAtRouteResult CreatedAtRoute (object routeValues, object value);

        [NonAction]
        public virtual CreatedAtRouteResult CreatedAtRoute (string routeName, object routeValues, object value);

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
        public virtual OkResult Ok ();

        [NonAction]
        public virtual OkObjectResult Ok (object value);

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
