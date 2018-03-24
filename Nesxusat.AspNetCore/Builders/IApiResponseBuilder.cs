using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Builders
{
    public interface IApiResponseBuilder: IApiResponseBuilderBase
    {
        IApiResponseBuilder SetHttpCode(int code);
        IApiResponseBuilder SetStatusCodeSuccess(string code);
        IApiResponseBuilder SetStatusCodeFailed(string code);
        IApiResponseBuilder SetException(Exception exception);
        IApiResponse Build();
    }
}
