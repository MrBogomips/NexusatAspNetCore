using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    public sealed class ExceptionInfo
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> StackTrace { get; set; }
        public static ExceptionInfo GetFromException(Exception exception)
        {
            if (null == exception) throw new ArgumentNullException(nameof(exception));

            return new ExceptionInfo
            {
                Type = exception.GetType().FullName,
                Message = exception.Message,
                StackTrace = exception.StackTrace?
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.TrimStart())
            };
        }
    }
}
