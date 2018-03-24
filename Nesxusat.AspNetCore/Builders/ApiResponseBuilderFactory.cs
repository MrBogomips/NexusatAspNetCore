using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Builders
{
    public class ApiResponseBuilderFactory : IApiResponseBuilderFactory
    {
        public IApiEnumResponseBuilder<T> GetApiEnumResponseBuilder<T>()
        {
            return new ApiEnumResponseBuilder<T>();
        }

        public IApiObjectResponseBuilder<T> GetApiObjectResponseBuilder<T>()
        {
            return new ApiObjectResponseBuilder<T>();
        }

        public IApiResponseBuilder GetApiResponseBuilder()
        {
            return new ApiResponseBuilder();
        }
    }
}
