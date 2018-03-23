using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Implementations;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal class ApiObjectResponseBuilder<T> : ApiResponseBuilderBase, IApiObjectResponseBuilder<T>
    {
        private IApiObjectResponse<T> Response => _response as IApiObjectResponse<T>;

        public ApiObjectResponseBuilder() : base(new ApiObjectResponse<T>()) { }

        public IApiObjectResponse<T> Build()
        {
            CheckBuildStateForFinalBuild();
            return Response;
        }

        public IApiObjectResponseBuilder<T> SetData(T data)
        {
            CheckBuildStateWhileBuilding();
            Response.Data = data;
            return this;
        }

        public IApiObjectResponseBuilder<T> SetHttpCode(int code)
        {
            InternalSetHttpCode(code);
            return this;
        }

        IApiObjectResponseBuilder<T> SetException(Exception exception)
        {
            InternalSetException(exception);
            return this;
        }

        IApiObjectResponseBuilder<T> SetStatusCodeFailed(string code)
        {
            InternalSetStatusCodeFailed(code);
            return this;
        }

        IApiObjectResponseBuilder<T> SetStatusCodeSuccess(string code)
        {
            InternalSetStatusCodeSuccess(code);
            return this;
        }

        IApiObjectResponseBuilder<T> IApiObjectResponseBuilder<T>.SetStatusCodeSuccess(string code)
        {
            throw new NotImplementedException();
        }

        IApiObjectResponseBuilder<T> IApiObjectResponseBuilder<T>.SetStatusCodeFailed(string code)
        {
            throw new NotImplementedException();
        }

        IApiObjectResponseBuilder<T> IApiObjectResponseBuilder<T>.SetException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
