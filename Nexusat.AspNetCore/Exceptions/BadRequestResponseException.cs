using System;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Exceptions
{
    /// <summary>
    /// An exception managed by the system as a BadRequest exception
    /// </summary>
    [Serializable]
    public class BadRequestResponseException: ApiResponseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        public BadRequestResponseException()
            : base(400, Models.StatusCode.BAD_REQUEST_STATUS_CODE) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="statusCode">An KO Status code.</param>
        public BadRequestResponseException(string statusCode)
            : base(400, statusCode) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="inner">Inner Exception</param>
        public BadRequestResponseException(Exception inner)
            : base(400, Models.StatusCode.BAD_REQUEST_STATUS_CODE, inner){ }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nexusat.AspNetCore.Exceptions.BadRequestResponseException"/> class.
        /// </summary>
        /// <param name="statusCode">An KO Status code.</param>
        /// <param name="inner">Inner Exception.</param>
        public BadRequestResponseException(string statusCode, Exception inner)
            : base(400, statusCode, inner) {}
    }
}
