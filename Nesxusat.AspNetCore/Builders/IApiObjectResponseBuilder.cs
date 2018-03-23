using Nesxusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Builders
{
    public interface IApiObjectResponseBuilder<T>: IApiResponseBuilderBase
    {
        ApiResponseBuilder SetData(T data);
        IApiObjectResponse<T> Build();
    }
}
