using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Models
{
    public class UnhandledExceptionResponse: ApiResponse
    {
        public UnhandledExceptionResponse(Exception exception)
            :base((int)HttpStatusCode.InternalServerError, CommonStatusCodes.UNHANDLED_EXCEPTION)
        {
            this.SetException(exception);
        }
    }
}
