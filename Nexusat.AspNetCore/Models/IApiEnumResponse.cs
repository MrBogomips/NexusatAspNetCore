using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nexusat.AspNetCore.Models;

public interface IApiEnumResponse : IApiResponseBase
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    PaginationInfo Navigation { get; }
}

/// <summary>
/// Represents a response with a list of objects
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IApiEnumResponse<T>: IApiEnumResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IEnumerable<T> Data { get; }
}