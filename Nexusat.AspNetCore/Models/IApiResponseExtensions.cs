using System;

namespace Nexusat.AspNetCore.Models;

public static class IApiResponseExtensions
{
    public static IApiResponse SetException(this IApiResponse response, Exception exception)
    {
        response.Exception = ExceptionInfo.GetFromException(exception);
        return response;
    }

    /* DELETE
    public static IApiResponse SetLocation(this IApiResponse response, string location)
    {
        response.Location = location ?? throw new ArgumentNullException(nameof(location));
        return response;
    }
    */
}