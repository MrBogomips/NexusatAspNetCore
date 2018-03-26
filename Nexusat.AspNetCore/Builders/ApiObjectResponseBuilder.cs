using System;
using System.Collections.Generic;
using System.Text;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;

namespace Nexusat.AspNetCore.Builders
{
    internal class ApiObjectResponseBuilder<T> : ApiResponseBuilderBase, IApiObjectResponseBuilder<T>
    {
        private IApiObjectResponse<T> Response => _response as IApiObjectResponse<T>;

        public ApiObjectResponseBuilder() : base(new ApiObjectResponse<T>()) { }

        public IApiObjectResponse<T> Build()
        {
            SingleInstanceChecker.CheckBuildStateForFinalBuild();
            return Response;
        }

        public IApiObjectResponseBuilder<T> SetData(T data)
        {
            SingleInstanceChecker.CheckBuildStateWhileBuilding();
            Response.Data = data;
            return this;
        }

        public IApiObjectResponseBuilder<T> SetHttpCode(int code)
        {
            InternalSetHttpCode(code);
            return this;
        }

        public IApiObjectResponseBuilder<T> SetException(Exception exception)
        {
            InternalSetException(exception);
            return this;
        }

        public IApiObjectResponseBuilder<T> SetStatusCodeFailed()
        {
            InternalSetStatusCodeFailed();
            return this;
        }

        public IApiObjectResponseBuilder<T> SetStatusCodeSuccess()
        {
            InternalSetStatusCodeSuccess();
            return this;
        }

        public IApiObjectResponseBuilder<T> SetStatusCode(string code)
        {
            InternalSetStatusCode(code);
            return this;
        }

        public IApiObjectResponseBuilder<T> SetDescription(string description)
        {
            InternalSetDescription(description);
            return this;
        }

        public IApiObjectResponseBuilder<T> SetUserDescription(string userDescription)
        {
            InternalSetUserDescription(userDescription);
            return this;
        }
    }
}
