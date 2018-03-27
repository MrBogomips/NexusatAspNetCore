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
    }
}
