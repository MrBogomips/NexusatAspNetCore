using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Nexusat.AspNetCore.Models;

public sealed class ExceptionInfo
{
        
    public string Type { get; set; }
    public string Message { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ExceptionInfo Inner { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string> StackTrace { get; set; }
    public static ExceptionInfo GetFromException(Exception exception)
    {
        if (null == exception) throw new ArgumentNullException(nameof(exception));

        return new ExceptionInfo
        {
            Type = exception.GetType().FullName,
            Message = exception.Message,
            Inner = GetInnerExceptions(exception.InnerException),
            StackTrace = exception.StackTrace?
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.TrimStart())
        };
    }
    private static ExceptionInfo GetInnerExceptions(Exception exception) {
        if (exception == null)
        {
            return null;
        }
        return new ExceptionInfo
        {
            Type = exception.GetType().FullName,
            Message = exception.Message,
            Inner = GetInnerExceptions(exception.InnerException)
        };
    }
}