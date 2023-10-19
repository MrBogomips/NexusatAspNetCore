using System.Text.Json.Serialization;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// Immutable class that represents a response with an object as payload.
/// </summary>
public class ApiObjectResponse<T> : ApiResponse, IApiObjectResponse<T>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; }

    public ApiObjectResponse(Status status, T data) : base(status) {
        Data = data;
    }

    public ApiObjectResponse(int httpCode, string statusCode = null, T data = default(T), string description = null, string userDescription = null)
        : this(new Status(httpCode, statusCode, description, userDescription), data) { }
        
}