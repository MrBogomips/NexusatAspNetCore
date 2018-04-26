using Newtonsoft.Json;
using Nexusat.AspNetCore.Properties;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Avoid this method in favor of
        /// <list type="bullet">
        ///     <item><see cref="SetSuccessCode"/> for success codes</item>
        ///     <item><see cref="SetFailedCode"/> for success codes</item>
        /// </list>
        /// whenever possibile.
        /// This method incur in performance overhead dued to validation logic.
        /// </summary>
        /// <param name="code"></param>
        public void SetCode(string code)
        {
            if (StatusCode.CheckValidCode(code))
            {
               Code = code;
            }
            else throw new ArgumentException(FormatSystemMessage(FormatSystemMessage(ExceptionMessages.StatusCodeInvalid, code)), nameof(code));
        }

        public void SetSuccessCode() => Code = CommonStatusCodes.OK;
        public void SetFailedCode() => Code = CommonStatusCodes.KO;
        public void SetSuccessCode(string subcode) => Code = StatusCode.GetStatusCodeSuccess(subcode);
        public void SetFailedCode(string subcode) => Code = StatusCode.GetStatusCodeFailed(subcode);

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
