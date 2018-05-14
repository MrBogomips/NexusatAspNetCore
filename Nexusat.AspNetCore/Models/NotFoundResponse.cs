using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Models
{
	/// <summary>
	/// A generic response for a not found route
	/// </summary>
	public class NotFoundResponse: ApiResponse
    {
        public NotFoundResponse()
            :base((int)HttpStatusCode.NotFound, CommonStatusCodes.NOT_FOUND_STATUS_CODE)
        { }
    }
}
