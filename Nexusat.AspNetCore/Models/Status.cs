using Newtonsoft.Json;
using Nexusat.AspNetCore.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Models
{

    public sealed class Status: IEquatable<Status>
    {
        public int HttpCode { get; set; }

        public StatusCode Code { get; set; } = StatusCode.Default;

        internal Status() { }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; internal set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserDescription { get; internal set; }

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
}
