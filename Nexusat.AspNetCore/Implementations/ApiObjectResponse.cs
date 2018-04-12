using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Implementations
{
    internal class ApiObjectResponse<T> : ApiResponse, IApiObjectResponse<T>
    {
        public T Data { get; set; }
    }
}
