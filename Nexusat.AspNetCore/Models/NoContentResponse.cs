using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// A generic response for a not found route
    /// </summary>
    public class NoContentResponse: ApiResponse
    {
        public NoContentResponse()
            : base((int)HttpStatusCode.NoContent)
        {
            HasBody = false;
        }
    }
}
