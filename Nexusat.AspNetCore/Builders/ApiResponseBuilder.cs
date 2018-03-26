using System;
using System.Collections.Generic;
using System.Text;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Builders
{
    internal class ApiResponseBuilder: ApiResponseBuilderBase, IApiResponseBuilder
    {
        private IApiResponseInternal Response { get => base._response; }

        public ApiResponseBuilder() : base(new ApiResponse())
        {

        }

        public IApiResponse Build()
        {
            SingleInstanceChecker.CheckBuildStateForFinalBuild();
            return Response as IApiResponse;
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

        public IApiResponseBuilder SetStatusCodeFailed()
        {
            InternalSetStatusCodeFailed();
            return this;
        }

        public IApiResponseBuilder SetStatusCodeSuccess()
        {
            InternalSetStatusCodeSuccess();
            return this;
        }

        public IApiResponseBuilder SetStatusCode(string code)
        {
            InternalSetStatusCode(code);
            return this;
        }

        public IApiResponseBuilder SetDescription(string description)
        {
            InternalSetDescription(description);
            return this;
        }

        public IApiResponseBuilder SetUserDescription(string userDescription)
        {
            InternalSetUserDescription(userDescription);
            return this;
        }
    }
}
