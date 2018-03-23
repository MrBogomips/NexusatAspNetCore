using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal abstract class ApiResponseBuilderBase: IApiResponseBuilderBase
    {
        public ApiResponseBuilderBase SetHttpCode(int code)
        {
            return this;
        }

        public ApiResponseBuilderBase SetStatusCodeSuccess(string code)
        {
            return this;
        }

        public ApiResponseBuilderBase SetStatusCodeFailed(string code)
        {

            return this;
        }

        public ApiResponseBuilderBase SetException(Exception exception)
        {
            return this;
        }
    }
}
