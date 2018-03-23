using System;
using System.Collections.Generic;
using System.Text;

namespace Nesxusat.AspNetCore.Builders
{
    public interface IApiResponseBuilderBase
    {
        ApiResponseBuilder SetHttpCode(int code);
        ApiResponseBuilder SetStatusCodeSuccess(string code);
        ApiResponseBuilder SetStatusCodeFailed(string code);
        ApiResponseBuilder SetException(Exception exception);
    }
}
