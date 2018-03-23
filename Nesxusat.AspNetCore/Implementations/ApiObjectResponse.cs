using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Implementations
{
    internal class ApiObjectResponse<T> : ApiResponse, IApiObjectResponse<T>
    {
        public T Data { get; set; }
    }
}
