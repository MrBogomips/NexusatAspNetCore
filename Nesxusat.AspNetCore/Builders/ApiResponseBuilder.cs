using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Implementations;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal class ApiResponseBuilder: ApiResponseBuilderBase, IApiResponseBuilder
    {
        private IApiResponse Response { get => base._response; }

        public ApiResponseBuilder() : base(new ApiResponse())
        {

        }

        public IApiResponse Build()
        {
            CheckBuildStateForFinalBuild();
            return Response;
        }

        public IApiResponseBuilder SetHttpCode(int code)
        {
            InternalSetHttpCode(code);
            return this;
        }

        public IApiResponseBuilder SetException(Exception exception)
        {
            InternalSetException(exception);
            return this;
        }

        public IApiResponseBuilder SetStatusCodeFailed(string code)
        {
            InternalSetStatusCodeFailed(code);
            return this;
        }

        public IApiResponseBuilder SetStatusCodeSuccess(string code)
        {
            InternalSetStatusCodeSuccess(code);
            return this;
        }
    }
}
