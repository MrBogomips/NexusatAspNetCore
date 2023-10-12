using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nexusat.AspNetCore.Models;

public interface IApiEnumResponse : IApiResponseBase
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    PaginationInfo Navigation { get; }
}

/// <summary>
/// Represents a response with a list of objects
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IApiEnumResponse<T>: IApiEnumResponse
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    IEnumerable<T> Data { get; }
}