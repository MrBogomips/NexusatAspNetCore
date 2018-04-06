using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Builders
{
    public interface IApiResponseBuilder: IApiResponseBuilderBase
    {
        IApiResponseBuilder SetHttpCode(int code);
        IApiResponseBuilder SetStatusCode(string code);
        IApiResponseBuilder SetStatusCodeSuccess();
        IApiResponseBuilder SetStatusCodeFailed();
        IApiResponseBuilder SetDescription(string description);
        IApiResponseBuilder SetUserDescription(string userDescription);
        IApiResponseBuilder SetException(Exception exception);
        IApiResponse GetResponse();
    }
}
