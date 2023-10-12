using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nexusat.AspNetCore.Exceptions;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Models;

public static class BadRequest {
    public const string ApiStatusCode = CommonStatusCodes.BAD_REQUEST;
    public const int HttpStatusCode = StatusCodes.Status400BadRequest;

    /// <summary>
    /// Bad request response.
    /// </summary>
    public class Response : Models.ApiResponse
    {
        ModelStateDictionary ModelState { get; set; }
            
        public Response(string statusCode = null, string description = null, string userDescription = null)
            : base(HttpStatusCode, statusCode ?? ApiStatusCode, description, userDescription)
        {
            if (statusCode != null) StatusCode.CheckValidKoCodeOrThrow(statusCode);
        }

        public Response(ModelStateDictionary modelStateDictionary, string statusCode = null, string description = null, string userDescription = null)
            : this(statusCode, description, userDescription)
        {
            ModelState = modelStateDictionary ?? throw new ArgumentNullException(nameof(modelStateDictionary));            
        }

        public override void OnFormatting(ActionContext context)
        {
            base.OnFormatting(context);

            if (ModelState != null)
                ValidationErrors = new ValidationErrorsInfo(context, ModelState);
        }
    }
	
    /// <summary>
    /// Bad request response with optional <see cref="Data"/> payload.
    /// </summary>
    public class Object<T> : Response, IApiObjectResponse<T>
    {
        public T Data { get; }

        public Object(string statusCode, T data = default(T), string description = null, string userDescription = null)
            : base(statusCode, description, userDescription)
        {
            Data = data;
        }

        public Object(T data = default(T), string description = null, string userDescription = null)
            : base(description, userDescription)
        {
            Data = data;
        }

        public Object(ModelStateDictionary modelState, T data = default(T))
            : base(modelState)
        {
            Data = data;
        }
    }

    public class Enum<T> : ApiEnumResponse<T>
    {
        public Enum(IEnumerable<T> data, PaginationCursor current, bool hasNextPage, string description = null, string userDescription = null)
            : base(HttpStatusCode, current, hasNextPage, data, statusCode: ApiStatusCode, description: description, userDescription: userDescription) { }
        public Enum(string statusCode, IEnumerable<T> data, PaginationCursor current, bool hasNextPage, string description = null, string userDescription = null)
            : base(HttpStatusCode, current, hasNextPage, data, statusCode: statusCode, description: description, userDescription: userDescription)
        {
            if (statusCode == null)
            {
                throw new ArgumentNullException(nameof(statusCode));
            }
            StatusCode.CheckValidOkCodeOrThrow(statusCode);
        }
        public Enum(IEnumerable<T> data, PaginationCursor current, int itemsCount, string description = null, string userDescription = null)
            : base(HttpStatusCode, current, itemsCount, data, statusCode: ApiStatusCode, description: description, userDescription: userDescription) { }
        public Enum(string statusCode, IEnumerable<T> data, PaginationCursor current, int itemsCount, string description = null, string userDescription = null)
            : base(HttpStatusCode, current, itemsCount, data, statusCode: statusCode, description: description, userDescription: userDescription)
        {
            if (statusCode == null)
            {
                throw new ArgumentNullException(nameof(statusCode));
            }
            StatusCode.CheckValidOkCodeOrThrow(statusCode);
        }
    }

    /// <summary>
    /// An exception managed by the system as a BadRequest exception
    /// </summary>
    [Serializable]
    public class Exception : ApiResponseException
    {
        public ModelStateDictionary ModelState { get; }

        public Exception(ModelStateDictionary modelState)
            : this()
        {
            ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        public Exception()
            : base(HttpStatusCode, ApiStatusCode) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="statusCode">An KO Status code.</param>
        public Exception(string statusCode)
            : base(HttpStatusCode, statusCode) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="inner">Inner Exception</param>
        public Exception(Exception inner)
            : base(HttpStatusCode, ApiStatusCode, inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="statusCode">An KO Status code.</param>
        /// <param name="inner">Inner Exception.</param>
        public Exception(string statusCode, Exception inner)
            : base(HttpStatusCode, statusCode, inner) { }

        public override ApiResponse GetResponse()
        {
            var response = ModelState != null ?
                new Response(ModelState, StatusCode, Description, UserDescription)
                : new Response(StatusCode, Description, UserDescription);
				
            if (InnerException != null)
            {
                response.Exception = ExceptionInfo.GetFromException(InnerException);
            }
            return response;
        }         
    }
}