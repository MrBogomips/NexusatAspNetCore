using System.Text.Json.Serialization;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// Represents a response with a single data object.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IApiObjectResponse<T>: IApiResponseBase
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    T Data { get; }

    /*
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    string Location { get; set; }
    */
}