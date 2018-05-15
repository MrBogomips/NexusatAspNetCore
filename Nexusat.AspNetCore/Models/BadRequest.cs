using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nexusat.AspNetCore.Exceptions;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Models
{

	public static class BadRequest {
		public const string ApiStatusCode = CommonStatusCodes.BAD_REQUEST;
		public const int HttpStatusCode = StatusCodes.Status400BadRequest;

		/// <summary>
        /// Bad request response.
        /// </summary>
        public class Response : Models.ApiResponse
        {
            private ModelStateDictionary ModelState { get; set; }
            public Response(string description = null, string userDescription = null)
				: this(ApiStatusCode, description, userDescription) { }

            public Response(string statusCode, string description = null, string userDescription = null)
                : base(HttpStatusCode, statusCode, description, userDescription)
            {
                StatusCode.CheckValidKoCodeOrThrow(statusCode);
            }

            public Response(ModelStateDictionary modelStateDictionary)
                : this()
            {
                ModelState = modelStateDictionary ?? throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            /// <summary>
            /// Gets a <see cref="Response"/> from an exception.
            /// </summary>
            /// <returns>The from exception.</returns>
            /// <param name="exception">The exception that will be encapsulated</param>
            public static Response GetFromException(BadRequestResponseException exception)
            {
                var response = new Response(exception.StatusCode, exception.Description, exception.UserDescription);
                response.ModelState = exception.ModelState;

                if (exception.InnerException != null)
                {
                    response.Exception = ExceptionInfo.GetFromException(exception.InnerException);
                }
                return response;
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

	}   
}
