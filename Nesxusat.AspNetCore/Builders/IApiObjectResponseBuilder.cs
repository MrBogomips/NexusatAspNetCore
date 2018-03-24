using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Builders
{
    public interface IApiObjectResponseBuilder<T>: IApiResponseBuilderBase
    {
        IApiObjectResponseBuilder<T> SetHttpCode(int code);
        IApiObjectResponseBuilder<T> SetStatusCodeSuccess();
        IApiObjectResponseBuilder<T> SetStatusCodeFailed();
        IApiObjectResponseBuilder<T> SetStatusCodeSuccess(string code);
        IApiObjectResponseBuilder<T> SetStatusCodeFailed(string code);
        IApiObjectResponseBuilder<T> SetException(Exception exception);
        IApiObjectResponseBuilder<T> SetData(T data);
        IApiObjectResponse<T> Build();
    }
}
