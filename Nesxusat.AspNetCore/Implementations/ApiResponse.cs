using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Implementations
{
    internal class ApiResponse<T> : IApiResponse<T> where T : class
    {
        public Status Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public T Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
