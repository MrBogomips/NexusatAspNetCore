using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Implementations
{
    /// <summary>
    /// A generic response for a not found route
    /// </summary>
    internal class NotFoundResponse: ApiResponse
    {
        public NotFoundResponse()
        {
            Status.HttpCode = (int)HttpStatusCode.NotFound;
            Status.SetCode(CommonStatusCodes.NOT_FOUND_STATUS_CODE);
        }
    }
}
