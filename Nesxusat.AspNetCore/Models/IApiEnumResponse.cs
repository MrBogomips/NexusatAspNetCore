using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Represents a response with a list of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiEnumResponse<T>: IApiResponse
    {
        NavigationInfo Navigation { get; set; }
        IEnumerable<T> Data { get; set; }
    }
}
