using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Implementations
{
    internal class ApiResponse : IApiResponse, IApiResponseInternal
    {
        public Status Status { get; set; } = new Status();
        public ExceptionInfo Exception { get; set; }
        public ValidationErrorsInfo ValidationErrors { get; set; }
        public RuntimeInfo Runtime { get; set; }
    }
}
