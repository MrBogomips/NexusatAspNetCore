using Nexusat.AspNetCore.Models;
using System;

namespace Nexusat.AspNetCore.Models
{
    public static class IApiObjectResponseExtensions
    {
        public static IApiObjectResponse<T> SetException<T>(this IApiObjectResponse<T> response, Exception exception)
        {
            response.Exception = ExceptionInfo.GetFromException(exception);
            return response;
        }

        public static IApiObjectResponse<T> SetLocation<T>(this IApiObjectResponse<T> response, string location)
        {
            response.Location = location ?? throw new ArgumentNullException(nameof(location));
            return response;
        }
    }
}
