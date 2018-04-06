using System;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Mvc
{
    public static class ApiResponseExtensions
    {
        public static IApiResponse SetException(this IApiResponse response, Exception exception) {
            ApiResponse r = (ApiResponse) response;
            r.Exception = ExceptionInfo.GetFromException(exception);
            return response;
        }

        public static IApiObjectResponse<T> SetException<T>(this IApiObjectResponse<T> response, Exception exception)
        {
            ApiObjectResponse<T> r = (ApiObjectResponse<T>)response;
            r.Exception = ExceptionInfo.GetFromException(exception);
            return response;
        }

        public static IApiEnumResponse<T> SetException<T>(this IApiEnumResponse<T> response, Exception exception)
        {
            ApiEnumResponse<T> r = (ApiEnumResponse<T>)response;
            r.Exception = ExceptionInfo.GetFromException(exception);
            return response;
        }
    }
}
