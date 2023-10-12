using Newtonsoft.Json;
using System;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// Immutable class that represents the status of the response
/// </summary>
public sealed class Status: IEquatable<Status>
{
    public int HttpCode { get; }

    public StatusCode Code { get; }

    public Status(int httpCode, string statusCode = null, string description = null, string userDescription = null) {
        HttpCode = httpCode;
        Code = statusCode ?? StatusCode.Default;
        Description = description;
        UserDescription = userDescription;
    }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Description { get; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string UserDescription { get; }

    #region Equals
    public override bool Equals(object obj) => Equals(obj as Status);
    public bool Equals(Status that) => 
        that != null
        && HttpCode == that.HttpCode
        && Code == that.Code 
        && Description == that.Description;
    public override int GetHashCode() => 
        HttpCode 
        ^ Code.GetHashCode() << 16
        ^ Description.GetHashCode();
    #endregion Equals
}