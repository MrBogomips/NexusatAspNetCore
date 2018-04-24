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

        public ApiResponseBuilder() : this(new ApiResponse()) { }
        public ApiResponseBuilder(IApiResponse obj) : base(obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        public IApiResponse GetResponse()
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

        public IApiResponseBuilder SetLocation(string location)
        {
            InternalSetLocation(location);
            return this;
        }
    }
}
