using System;
using System.Net;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Exceptions
{
    /// <summary>
    /// An exception managed by the system as a NoContent exception (HTTP 204)
    /// </summary>
    [Serializable]
    public class NoContentResponseException: ApiResponseException
    {
        private const int httpCode = (int)HttpStatusCode.NoContent;
        private const string statusCode = CommonStatusCodes.OK_INTERNAL_PIZZA;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        public NoContentResponseException()
            : base(httpCode, statusCode) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="inner">Inner Exception</param>
        public NoContentResponseException(Exception inner)
            : base(httpCode, statusCode, inner){ }
    }
}
