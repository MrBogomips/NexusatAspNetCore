using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Builders
{
    public interface IApiResponseBuilderFactory
    {
        IApiResponseBuilder GetApiResponseBuilder();
        IApiObjectResponseBuilder<T> GetApiObjectResponseBuilder<T>();
        IApiEnumResponseBuilder<T> GetApiEnumResponseBuilder<T>();
    }
}
