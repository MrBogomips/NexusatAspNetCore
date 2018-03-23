using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal abstract class ApiResponseBuilderBase: IApiResponseBuilderBase
    {
        private readonly IApiResponse Response;

        protected ApiResponseBuilderBase(IApiResponse response)
        {
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }

        protected void InternalSetHttpCode(int code)
        {
            throw new NotImplementedException();
        }

        protected void SetStatusCodeSuccess(string code)
        {
            throw new NotImplementedException();
        }

        protected void SetStatusCodeFailed(string code)
        {
            throw new NotImplementedException();
        }

        protected void SetException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
