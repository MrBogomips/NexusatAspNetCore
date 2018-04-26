using Nexusat.AspNetCore.Models;
using System;

namespace Nexusat.AspNetCore.Models
{
    public static class IApiEnumResponseExtensions
    {
        public static IApiEnumResponse<T> SetException<T>(this IApiEnumResponse<T> response, Exception exception)
        {
            response.Exception = ExceptionInfo.GetFromException(exception);
            return response;
        }
    }
}
