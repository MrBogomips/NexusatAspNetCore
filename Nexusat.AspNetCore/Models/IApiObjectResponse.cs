﻿using Newtonsoft.Json;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// Represents a response with a single data object.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IApiObjectResponse<T>: IApiResponseBase
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    T Data { get; }

    /*
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    string Location { get; set; }
    */
}