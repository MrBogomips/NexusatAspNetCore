using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Implementations
{
    internal class ApiEnumResponse<T> : ApiResponse, IApiEnumResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public NavigationInfo Navigation { get; set; }
    }
}
