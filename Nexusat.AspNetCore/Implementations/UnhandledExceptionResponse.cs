using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Implementations
{
    internal class UnhandledExceptionResponse: ApiResponse
    {
        public UnhandledExceptionResponse(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            Status.HttpCode = (int)HttpStatusCode.InternalServerError;
            Status.SetCode(CommonStatusCodes.UNHANDLED_EXCEPTION_STATUS_CODE);
            this.Exception = ExceptionInfo.GetFromException(exception);
        }
    }
}
