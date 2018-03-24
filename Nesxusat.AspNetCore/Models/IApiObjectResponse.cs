using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Models
{
    /// <summary>
    /// Represents a response with a single data object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiObjectResponse<T>: IApiResponse
    {
        T Data { get; set; }
    }
}
