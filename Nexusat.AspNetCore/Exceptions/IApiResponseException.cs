using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Exceptions;

/// <summary>
/// Implementing this interface allows the thrower to produce a custom ApiResponse message
/// instead of the default <see cref="Models.UnhandledExceptionResponse"/>.
/// </summary>
public interface IApiResponseException
{      
	/// <summary>
	/// Will return an instance of <see cref="ApiResponse"/> or a subclass.
	/// </summary>
	/// <returns>The response.</returns>
	ApiResponse GetResponse();
}