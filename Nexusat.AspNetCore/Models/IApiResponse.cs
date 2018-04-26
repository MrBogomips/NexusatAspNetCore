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
    public interface IApiResponse: IApiResponseBase
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string Location { get; set; }
    }
}
