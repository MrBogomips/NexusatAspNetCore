using Newtonsoft.Json;
using Nexusat.AspNetCore.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Helper class to build valid Status Codes.
    /// </summary>
    internal static class StatusCode
    {
        public static bool CheckValidCode(string code) =>
            code != null && (
                code == "OK" ||
                code == "KO" ||
                code.StartsWith("OK_") ||
                code.StartsWith("KO_")
            );
        public static string GetStatusCodeSuccess(string subcode) => string.Format("OK_{0}", FormatSubCode(subcode));
        public static string GetStatusCodeFailed(string subcode) => string.Format("OK_{0}", FormatSubCode(subcode));

        private static string FormatSubCode(string subcode)
        {
            if (string.IsNullOrWhiteSpace(subcode))
                throw new ArgumentException(FormatSystemMessage(ExceptionMessages.SubCodeInvalidFormat, nameof(subcode)));
            return subcode.Replace(' ', '_').ToUpperInvariant();
        }
    }

    public sealed class Status
    {
        #region Common Statuses
        internal static Status Ok
        {
            get => new Status
            {
                HttpCode = 200,
                Code = "OK"
            };
        }
        public static Status Ko
        {
            get => new Status
            {
                HttpCode = 200,
                Code = "KO"
            };
        }
        #endregion Common Statuses

        public int HttpCode { get; internal set; }
        public string Code { get; internal set; }
        public string Description { get; internal set; }
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
            ^ Code.GetHashCode() 
            ^ Description.GetHashCode();
        #endregion Equals
    }
}
