using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal class ApiEnumResponseBuilder<T> : ApiResponseBuilderBase, IApiEnumResponseBuilder<T>
    {
        public IApiEnumResponse<T> Build()
        {
            throw new NotImplementedException();
        }

        public IApiEnumResponseBuilder<T> SetData(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }

        
    }
}
