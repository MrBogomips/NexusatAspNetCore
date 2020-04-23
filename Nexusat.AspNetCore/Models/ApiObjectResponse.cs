using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Immutable class that represents a response with an object as payload.
    /// </summary>
    public class ApiObjectResponse<T> : ApiResponse, IApiObjectResponse<T>
    {
        public T Data { get; }

        public ApiObjectResponse(Status status, T data) : base(status) {
            Data = data;
        }

        public ApiObjectResponse(int httpCode, string statusCode = null, T data = default(T), string description = null, string userDescription = null)
            : this(new Status(httpCode, statusCode, description, userDescription), data) { }
        
    }
}
