using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Base interface that encapsulate meta information
    /// carried out for each response
    /// </summary>
    public interface IApiResponse
    {
        Status Status { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ExceptionInfo Exception { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ValidationErrorsInfo ValidationErrors { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        RuntimeInfo Runtime { get; }
    }

    internal interface IApiResponseInternal
    {
        Status Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ExceptionInfo Exception { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ValidationErrorsInfo ValidationErrors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        RuntimeInfo Runtime { get; set; }
    }
}
