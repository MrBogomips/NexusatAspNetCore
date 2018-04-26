using System;
using System.Net;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Bad request response. For internal purposes.
    /// </summary>
    public class BadRequestResponse: ApiResponse
    {
        public BadRequestResponse(string description = null, string userDescription = null)
            :this(CommonStatusCodes.BAD_REQUEST_STATUS_CODE, description, userDescription) {}

        public BadRequestResponse(string statusCode, string description = null, string userDescription = null)
            :base((int)HttpStatusCode.BadRequest, statusCode, description, userDescription)
        { }

        /// <summary>
        /// Gets a <see cref="BadRequestResponse"/> from an exception.
        /// </summary>
        /// <returns>The from exception.</returns>
        /// <param name="exception">The exception that will be encapsulated</param>
        public static BadRequestResponse GetFromException(BadRequestResponseException exception) {
            var response = new BadRequestResponse(exception.StatusCode, exception.Description, exception.UserDescription);
            if (exception.InnerException != null)
            {
                response.Exception = ExceptionInfo.GetFromException(exception.InnerException);
            }
            return response;
        }
    }
}
