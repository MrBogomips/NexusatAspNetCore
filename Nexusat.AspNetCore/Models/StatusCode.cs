
using Nexusat.AspNetCore.Properties;
using System;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Models;

/// <summary>
/// Immutable class to represent a status code.
/// You can think of it as a syntax constrained string in fact it's 
/// equipped with implicit conversion operators 'to' and 'from' string class.
/// Provided also with helper methods for string validation.
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(StatusCodeConverter))]
public class StatusCode : IEquatable<StatusCode>, IEquatable<string>
{
    /// <summary>
    /// The Default status code
    /// </summary>
    public static readonly StatusCode Default = CommonStatusCodes.UNK;
    public static readonly StatusCode Ok = CommonStatusCodes.OK;
    public static readonly StatusCode Ko = CommonStatusCodes.KO;

    public string Code { get; }
    public StatusCode(string code)
    {
        CheckValidCodeOrThrow(code);
        Code = code;
    }

    public override string ToString() => Code;

    #region Equality
    public bool Equals(string other) => other.Equals(Code);
    public bool Equals(StatusCode other) => !ReferenceEquals(other, null) && other.Code.Equals(Code);
    public override bool Equals(object obj) => Equals(obj as string);
    public override int GetHashCode() => Code.GetHashCode();

    public static bool operator == (StatusCode lhs, StatusCode rhs) {
        if (ReferenceEquals(lhs, rhs))
        {
            return true;
        }
        if (ReferenceEquals(lhs,null) || ReferenceEquals(rhs,null))
        {
            return false;
        }
        string slhs = lhs.Code;
        string srhs = rhs.Code;
        return slhs.Equals(srhs);
    }

    public static bool operator !=(StatusCode lhs, StatusCode rhs) => !(lhs == rhs);

    #endregion Equality

    #region String to and from implicit conversions 
    //public static implicit operator string(StatusCode code) => code.Code;
    public static implicit operator StatusCode(string code) => new StatusCode(code);
    #endregion String to and from implicit conversions 

    #region static members
    /// <summary>
    /// Checks that the code is a valid successful code.
    /// </summary>
    /// <returns><c>true</c>, if ok valid code was checked, <c>false</c> otherwise.</returns>
    /// <param name="code">Code.</param>
    public static bool CheckOkValidCode(string code) =>
        code != null && (
            code == CommonStatusCodes.OK ||
            code.StartsWith(CommonStatusCodes.OK_, StringComparison.InvariantCulture)
        );
    /// <summary>
    /// Checks that the code is a valid unsuccessful code.
    /// </summary>
    /// <returns><c>true</c>, if ko valid code was checked, <c>false</c> otherwise.</returns>
    /// <param name="code">Code.</param>
    public static bool CheckKoValidCode(string code) =>
        code != null && (
            code == CommonStatusCodes.KO ||
            code.StartsWith(CommonStatusCodes.KO_, StringComparison.InvariantCulture)
        );
    /// <summary>
    /// Checks that the code is sinthactically valid.
    /// </summary>
    /// <returns><c>true</c>, if valid code was checked, <c>false</c> otherwise.</returns>
    /// <param name="code">Code.</param>
    public static bool CheckValidCode(string code) =>
        code != null && (
            code == CommonStatusCodes.OK ||
            code == CommonStatusCodes.KO ||
            code.StartsWith(CommonStatusCodes.OK_, StringComparison.InvariantCulture) ||
            code.StartsWith(CommonStatusCodes.KO_, StringComparison.InvariantCulture)
        );
    /// <summary>
    /// Checks that the code is valid or throws an Exception
    /// </summary>
    /// <param name="code">Code.</param>
    public static void CheckValidCodeOrThrow(string code)
    {
        if (!CheckValidCode(code)) throw new ArgumentException(FormatSystemMessage(ExceptionMessages.StatusCodeInvalid, code), nameof(code));
    }
    /// <summary>
    /// Checks that the code is a valid "OK" code or throws an Exception
    /// </summary>
    /// <param name="code">Code.</param>
    public static void CheckValidOkCodeOrThrow(string code)
    {
        if (!CheckOkValidCode(code)) throw new ArgumentException(FormatSystemMessage(ExceptionMessages.StatusOkCodeInvalid, code), nameof(code));
    }
    /// <summary>
    /// Checks that the code is a valid "KO" code or throws an Exception
    /// </summary>
    /// <param name="code">Code.</param>
    public static void CheckValidKoCodeOrThrow(string code)
    {
        if (!CheckKoValidCode(code)) throw new ArgumentException(FormatSystemMessage(ExceptionMessages.StatusKoCodeInvalid, code), nameof(code));
    }

    /// <summary>
    /// Returns a valid success (OK_*) code
    /// </summary>
    /// <returns>The status code.</returns>
    /// <param name="subcode">Subcode.</param>
    public static StatusCode GetStatusCodeSuccess(string subcode) => string.Format("{0}{1}", CommonStatusCodes.OK_, FormatSubCode(subcode));
    /// <summary>
    /// Returns a valid failure (KO_*) code
    /// </summary>
    /// <returns>The status code.</returns>
    /// <param name="subcode">Subcode.</param>
    public static StatusCode GetStatusCodeFailed(string subcode) => string.Format("{0}{1}", CommonStatusCodes.KO_, FormatSubCode(subcode));

    private static string FormatSubCode(string subcode)
    {
        if (string.IsNullOrWhiteSpace(subcode))
            throw new ArgumentException(FormatSystemMessage(ExceptionMessages.SubCodeInvalidFormat, nameof(subcode)));
        return subcode.Replace(' ', '_').ToUpperInvariant();
    }


    #endregion static members
}