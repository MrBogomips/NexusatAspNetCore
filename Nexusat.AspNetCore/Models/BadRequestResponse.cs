using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Models
{

	public static class BadRequest {
		/// <summary>
        /// Bad request response.
        /// </summary>
        public class ApiResponse : Models.ApiResponse
        {
            private ModelStateDictionary ModelState { get; set; }
            public ApiResponse(string description = null, string userDescription = null)
                : this(CommonStatusCodes.BAD_REQUEST, description, userDescription) { }

            public ApiResponse(string statusCode, string description = null, string userDescription = null)
                : base((int)HttpStatusCode.BadRequest, statusCode, description, userDescription)
            {
                StatusCode.CheckValidKoCodeOrThrow(statusCode);
            }

            public ApiResponse(ModelStateDictionary modelStateDictionary)
                : this()
            {
                ModelState = modelStateDictionary ?? throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            /// <summary>
            /// Gets a <see cref="ApiResponse"/> from an exception.
            /// </summary>
            /// <returns>The from exception.</returns>
            /// <param name="exception">The exception that will be encapsulated</param>
            public static ApiResponse GetFromException(BadRequestResponseException exception)
            {
                var response = new ApiResponse(exception.StatusCode, exception.Description, exception.UserDescription);
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
		public class ObjectResponse<T> : ApiResponse, IApiObjectResponse<T>
        {
            public T Data { get; }

            public ObjectResponse(string statusCode, T data = default(T), string description = null, string userDescription = null)
                : base(statusCode, description, userDescription)
            {
                Data = data;
            }

            public ObjectResponse(T data = default(T), string description = null, string userDescription = null)
                : base(description, userDescription)
            {
                Data = data;
            }

            public ObjectResponse(ModelStateDictionary modelState, T data = default(T))
                : base(modelState)
            {
                Data = data;
            }
        }

	}   
}
