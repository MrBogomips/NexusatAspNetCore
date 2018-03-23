using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Builders
{
    public interface IApiEnumResponseBuilder<T>: IApiResponseBuilderBase
    {
        ApiResponseBuilder SetData(IEnumerable<T> data);
        IApiEnumResponse<T> Build();
    }
}
