using System;
using System.Collections.Generic;
using Nexusat.AspNetCore.Exceptions;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace Nexusat.AspNetCore.Models;

public static class NoContent {
	public const int HttpStatusCode = StatusCodes.Status204NoContent;

	public static readonly BaseResponse Response = new BaseResponse();

	/// <summary>
	/// A generic response for a not found route
	/// </summary>
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class BaseResponse : ApiResponse
	{
		internal BaseResponse()
			: base(HttpStatusCode)
		{
			HasBody = false;
		}
	}

	public class Object<T> : BaseResponse, IApiObjectResponse<T>
	{
		public T Data => default(T);
		public PaginationInfo Navigation => null;
	}

	public class Enum<T> : BaseResponse, IApiEnumResponse<T>
	{
		public IEnumerable<T> Data => null;
		public PaginationInfo Navigation => null;
	}

	/// <summary>
	/// An exception managed by the system as a NoContent exception (HTTP 204)
	/// </summary>
	[Serializable]
	public class Exception : System.Exception, IApiResponseException
	{         
		public ApiResponse GetResponse() => Response;
	}

}