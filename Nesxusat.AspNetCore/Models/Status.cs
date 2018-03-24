using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    internal static class StatusCode
    {
        public static bool CheckValidCode(string code) =>
            code != null && (
                code == "OK" ||
                code == "KO" ||
                code.StartsWith("OK_") ||
                code.StartsWith("KO_")
            );
        public static string GetStatusCodeSuccess(string subcode)
        {
            if (string.IsNullOrWhiteSpace(subcode)) throw new ArgumentException("Subcode must be a non blank or null string", nameof(subcode));
            return string.Format("OK_{0}", subcode);
        }
        public static string GetStatusCodeFailed(string subcode)
        {
            if (string.IsNullOrWhiteSpace(subcode)) throw new ArgumentException("Subcode must be a non blank or null string", nameof(subcode));
            return string.Format("KO_{0}", subcode);
        }
    }

    public sealed class Status
    {
        #region Common Statuses
        public static Status Ok { get; } = new Status
        {
            HttpCode = 200,
            Code = "OK"
        };
        public static Status Ko { get; } = new Status
        {
            HttpCode = 200,
            Code = "KO"
        };
        #endregion Common Statuses

        public int HttpCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UserDescription { get; set; }

        #region Equals
        public bool Equals(Status that) => that != null && Code == that.Code && Description == that.Code;
        public override bool Equals(object obj) => Equals(obj as Status);
        public override int GetHashCode() => HttpCode ^ Description.GetHashCode();
        #endregion Equals
    }
}
