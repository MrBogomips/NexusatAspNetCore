using Newtonsoft.Json;

namespace Nexusat.AspNetCore.Models;

public interface IApiResponseBase
{
    Status Status { get; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    ExceptionInfo Exception { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    ValidationErrorsInfo ValidationErrors { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    RuntimeInfo Runtime { get; set; }

    /// <summary>
    /// For responses for wich the body will not be produced.
    /// </summary>
    /// <value><c>true</c> if has body; otherwise, <c>false</c>.</value>
    [JsonIgnore]
    bool HasBody { get; set; }
}