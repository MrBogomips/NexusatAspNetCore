using Newtonsoft.Json;
using Nexusat.AspNetCore.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Helper class to build valid Status Codes.
    /// </summary>
    public static class StatusCode
    {

        public static bool CheckOkValidCode(string code) =>
            code != null && (
                code == CommonStatusCodes.OK ||
            code.StartsWith(CommonStatusCodes.OK_, StringComparison.InvariantCulture)
            );
        public static bool CheckKoValidCode(string code) =>
            code != null && (
            code == CommonStatusCodes.KO ||
            code.StartsWith(CommonStatusCodes.KO_, StringComparison.InvariantCulture)
            );
        public static bool CheckValidCode(string code) =>
            code != null && (
            code == CommonStatusCodes.OK ||
            code == CommonStatusCodes.KO ||
            code.StartsWith(CommonStatusCodes.OK_, StringComparison.InvariantCulture) ||
            code.StartsWith(CommonStatusCodes.KO_, StringComparison.InvariantCulture)
            );
        public static void CheckValidCodeOrThrow(string code)
	    {
            if (!CheckValidCode(code)) throw new ArgumentException(FormatSystemMessage(ExceptionMessages.StatusCodeInvalid), nameof(code));
        }

        public static void CheckValidOkCodeOrThrow(string code)
        {
            if (!CheckOkValidCode(code)) throw new ArgumentException(FormatSystemMessage(ExceptionMessages.StatusOkCodeInvalid), nameof(code));
        }

        public static void CheckValidKoCodeOrThrow(string code) 
        {
            if (!CheckKoValidCode(code)) throw new ArgumentException(FormatSystemMessage(ExceptionMessages.StatusKoCodeInvalid), nameof(code));
        }


        public static string GetStatusCodeSuccess(string subcode) => string.Format("{0}{1}", CommonStatusCodes.OK_, FormatSubCode(subcode));
        public static string GetStatusCodeFailed(string subcode) => string.Format("{0}{1}", CommonStatusCodes.KO_, FormatSubCode(subcode));

        private static string FormatSubCode(string subcode)
        {
            if (string.IsNullOrWhiteSpace(subcode))
                throw new ArgumentException(FormatSystemMessage(ExceptionMessages.SubCodeInvalidFormat, nameof(subcode)));
            return subcode.Replace(' ', '_').ToUpperInvariant();
        }
    }

    public sealed class Status: IEquatable<Status>
    {
        public int HttpCode { get; internal set; }

        public string Code { get; internal set; } = CommonStatusCodes.DEFAULT_UNK_STATUS_CODE;

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
