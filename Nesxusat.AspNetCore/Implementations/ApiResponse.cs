using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Implementations
{
    internal class ApiResponse<T> : IApiObjectResponse<T> where T : class
    {
        public Status Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public T Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ExceptionInfo Exception { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ValidationErrorsInfo ValidationErrors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
