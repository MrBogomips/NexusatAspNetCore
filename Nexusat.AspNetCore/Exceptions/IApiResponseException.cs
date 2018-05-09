using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Exceptions
{
    /// <summary>
    /// Implementing this interface allows the thrower to produce a custom ApiResponse message
    /// instead of the default <see cref="Models.UnhandledExceptionResponse"/>.
    /// </summary>
    public interface IApiResponseException
    {
        string StatusCode { get; }
        string Description { get; }
        string UserDescription { get; }
        int HttpCode { get; }
        bool HasBody { get; }
    }
}
