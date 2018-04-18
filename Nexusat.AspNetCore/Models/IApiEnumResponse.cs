using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{

    public interface IApiEnumResponse: IApiResponse {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        PaginationInfo Navigation { get; set; }
    }

    /// <summary>
    /// Represents a response with a list of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiEnumResponse<T>: IApiEnumResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IEnumerable<T> Data { get; set; }
    }
}
