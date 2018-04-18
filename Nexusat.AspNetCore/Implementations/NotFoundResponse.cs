using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Implementations
{
    /// <summary>
    /// A generic response for a not found route
    /// </summary>
    internal class NoContentResponse: ApiResponse
    {
        public NoContentResponse()
        {
            Status.HttpCode = (int)HttpStatusCode.NoContent;
            //Status.Code = CommonStatusCodes.NOT_FOUND_STATUS_CODE;
            HasBody = false;
        }
    }
}
