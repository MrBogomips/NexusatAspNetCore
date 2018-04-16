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
    internal static class StatusCode
    {
        /*
         * ATTENTION!!!!! 
         * 
         * Initialization order for static fields matters!!!
         * 
         * Never declare a static field that depends on a field declared later on!
         * Even with strings order matters!
         */
        public static readonly string OK = "OK";
        public static readonly string KO = "KO";
        private static readonly string OK_ = OK + "_";
        private static readonly string KO_ = KO + "_";

        /// <summary>
        /// The default status code used in case of successful operations
        /// </summary>
        public static readonly string DEFAULT_OK_STATUS_CODE = OK + "_DEFAULT";
        /// <summary>
        /// The default status code used in case of failed operations
        /// </summary>
        public static readonly string DEFAULT_KO_STATUS_CODE = KO + "_DEFAULT";
        /// <summary>
        /// The NotFound status code.
        /// </summary>
        public static readonly string NOT_FOUND_STATUS_CODE = KO + "_NOT_FOUND";
        /// <summary>
        /// The BadRequest status code.
        /// </summary>
        public static readonly string BAD_REQUEST_STATUS_CODE = KO + "_BAD_REQUEST";
        /// <summary>
        /// The unhandled exception status code.
        /// </summary>
        public static readonly string UNHANDLED_EXCEPTION_STATUS_CODE = KO + "_UNHANDLED_EXCEPTION";
        
        /// <summary>
        /// The default status code used in case of ambigous operations not specifically configured by the user
        /// </summary>
        public static readonly string DEFAULT_UNK_STATUS_CODE = DEFAULT_KO_STATUS_CODE + "_UNK";

        public static bool CheckOkValidCode(string code) =>
            code != null && (
                code == OK ||
                code.StartsWith(OK_, StringComparison.InvariantCulture)
            );
        public static bool CheckKoValidCode(string code) =>
            code != null && (
                code == KO ||
                code.StartsWith(KO_, StringComparison.InvariantCulture)
            );
        public static bool CheckValidCode(string code) =>
            code != null && (
                code == OK ||
                code == KO ||
                code.StartsWith(OK_, StringComparison.InvariantCulture) ||
                code.StartsWith(KO_, StringComparison.InvariantCulture)
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


        public static string GetStatusCodeSuccess(string subcode) => string.Format("{0}{1}", OK_, FormatSubCode(subcode));
        public static string GetStatusCodeFailed(string subcode) => string.Format("{0}{1}", KO_, FormatSubCode(subcode));

        private static string FormatSubCode(string subcode)
        {
            if (string.IsNullOrWhiteSpace(subcode))
                throw new ArgumentException(FormatSystemMessage(ExceptionMessages.SubCodeInvalidFormat, nameof(subcode)));
            return subcode.Replace(' ', '_').ToUpperInvariant();
        }
    }

    public sealed class Status
    {
        public int HttpCode { get; internal set; }

        private string _Code = StatusCode.DEFAULT_UNK_STATUS_CODE;

        public string Code {
            get => _Code;
            internal set {
                _Code = value;
            }
        }

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
                _Code = code.Trim().ToUpperInvariant() ;
            }
            else throw new ArgumentException(FormatSystemMessage(FormatSystemMessage(ExceptionMessages.StatusCodeInvalid, code)), nameof(code));
        }
        public void SetSuccessCode() => _Code = StatusCode.OK;
        public void SetFailedCode() => _Code = StatusCode.KO;
        public void SetSuccessCode(string subcode) => _Code = StatusCode.GetStatusCodeSuccess(subcode);
        public void SetFailedCode(string subcode) => _Code = StatusCode.GetStatusCodeFailed(subcode);

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
            ^ Code.GetHashCode() 
            ^ Description.GetHashCode();
        #endregion Equals
    }
}
