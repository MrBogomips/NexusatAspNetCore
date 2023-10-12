﻿using System.Globalization;

namespace Nexusat.AspNetCore.Utils;

/// <summary>
/// Utility class to format messages
/// </summary>
public static class StringFormatter
{
    /// <summary>
    /// Formats messages generated by the system for diagnostics
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string FormatSystemMessage(string message, params object[] args)
    {
        return string.Format(CultureInfo.InvariantCulture, message, args);
    }
    public static string FormatSystemMessage(string message, object arg)
    {
        return string.Format(CultureInfo.InvariantCulture, message, arg);
    }
    public static string FormatSystemMessage(string message, object arg0, object arg1)
    {
        return string.Format(CultureInfo.InvariantCulture, message, arg0, arg1);
    }
    public static string FormatSystemMessage(string message, object arg0, object arg1, object arg2)
    {
        return string.Format(CultureInfo.InvariantCulture, message, arg0, arg1, arg2);
    }
    /// <summary>
    /// Formats messages to the End User taking into account the culture info of the request
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string FormatUserMessage(string message, params object[] args)
    {
        return string.Format(message, args);
    }
    public static string FormatUserMessage(string message, object arg)
    {
        return string.Format(message, arg);
    }
    public static string FormatUserMessage(string message, object arg0, object arg1)
    {
        return string.Format(message, arg0, arg1);
    }
    public static string FormatUserMessage(string message, object arg0, object arg1, object arg2)
    {
        return string.Format(message, arg0, arg1, arg2);
    }
}