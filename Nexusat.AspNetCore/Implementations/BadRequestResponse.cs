using System;
using System.Net;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Implementations
{
    /// <summary>
    /// Bad request response. For internal purposes.
    /// </summary>
    internal class BadRequestResponse: ApiResponse
    {
        public BadRequestResponse(string description = null, string userDescription = null)
        {
            Status.HttpCode = (int)HttpStatusCode.BadRequest;
            Status.Code = StatusCode.BAD_REQUEST_STATUS_CODE;
            Status.Description = description;
            Status.UserDescription = userDescription;
        }

        internal static BadRequestResponse GetFromException(BadRequestResponseException exception) {
            var response = new BadRequestResponse(exception.Description, exception.UserDescription);
            response.Status.Code = exception.StatusCode;
            if (exception.InnerException != null)
            {
                response.Exception = ExceptionInfo.GetFromException(exception.InnerException);
            }
            return response;
        }
    }
}
