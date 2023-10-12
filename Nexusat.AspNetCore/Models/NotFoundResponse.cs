using System.Net;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// A generic response for a not found route
/// </summary>
public class NotFoundResponse: ApiResponse
{
	public NotFoundResponse()
		:base((int)HttpStatusCode.NotFound, CommonStatusCodes.NOT_FOUND)
	{ }
}