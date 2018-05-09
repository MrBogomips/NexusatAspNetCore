using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Models
{
	/// <summary>
	/// Bad request response.
	/// </summary>
	public class BadRequestResponse: ApiResponse
    {
        private ModelStateDictionary ModelState { get; set; }
        public BadRequestResponse(string description = null, string userDescription = null)
            :this(CommonStatusCodes.BAD_REQUEST_STATUS_CODE, description, userDescription) {}

        public BadRequestResponse(string statusCode, string description = null, string userDescription = null)
            :base((int)HttpStatusCode.BadRequest, statusCode, description, userDescription)
        {
			StatusCode.CheckValidKoCodeOrThrow(statusCode);
		}

        public BadRequestResponse(ModelStateDictionary modelStateDictionary)
            :this()
        {
            ModelState = modelStateDictionary ?? throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        /// <summary>
        /// Gets a <see cref="BadRequestResponse"/> from an exception.
        /// </summary>
        /// <returns>The from exception.</returns>
        /// <param name="exception">The exception that will be encapsulated</param>
        public static BadRequestResponse GetFromException(BadRequestResponseException exception) {         
			var response = new BadRequestResponse(exception.StatusCode, exception.Description, exception.UserDescription);
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
}
