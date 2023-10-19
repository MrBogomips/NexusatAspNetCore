using System.Text.Json.Serialization;

namespace Nexusat.AspNetCore.Models;

public interface IApiResponseBase
{
    Status Status { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    ExceptionInfo Exception { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    ValidationErrorsInfo ValidationErrors { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    RuntimeInfo Runtime { get; set; }

    /// <summary>
    /// For responses for wich the body will not be produced.
    /// </summary>
    /// <value><c>true</c> if has body; otherwise, <c>false</c>.</value>
    [JsonIgnore]
    bool HasBody { get; set; }
}