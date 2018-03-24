using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Represents a response with a list of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiEnumResponse<T>: IApiResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        NavigationInfo Navigation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IEnumerable<T> Data { get; set; }
    }
}
