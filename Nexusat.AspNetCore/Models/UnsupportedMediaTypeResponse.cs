using System.Net;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// A generic response for an unsupported media type request
/// </summary>
public class UnsupportedMediaTypeResponse : ApiResponse
{
    public UnsupportedMediaTypeResponse()
        : base((int)HttpStatusCode.UnsupportedMediaType, CommonStatusCodes.UNSUPPORTED_MEDIA_TYPE)
    { }
}