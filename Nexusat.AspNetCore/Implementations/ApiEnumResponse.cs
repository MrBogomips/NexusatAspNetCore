using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Implementations
{
    internal class ApiEnumResponse<T> : ApiResponse, IApiEnumResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public NavigationInfo Navigation { get; set; }

        public ApiEnumResponse() { }
        public ApiEnumResponse(IEnumerable<T> obj) { Data = obj; }
    }
}
