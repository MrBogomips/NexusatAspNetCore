using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Implementations
{
    internal class ApiResponse : IApiResponse
    {
        public Status Status { get; set; } = new Status();
        public ExceptionInfo Exception { get; set; }
        public ValidationErrorsInfo ValidationErrors { get; set; }
        public RuntimeInfo Runtime { get; set; }
    }
}
