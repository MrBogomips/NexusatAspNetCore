using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal class ApiObjectResponseBuilder<T> : ApiResponseBuilderBase, IApiObjectResponseBuilder<T>
    {
        public IApiObjectResponse<T> Build()
        {
            throw new NotImplementedException();
        }

        public IApiObjectResponseBuilder<T> SetData(T data)
        {
            throw new NotImplementedException();
        }

        public IApiObjectResponseBuilder<T> SetHttpCode(int code)
        {
            InternalSetHttpCode(code);
            return this;
        }

        IApiObjectResponseBuilder<T> IApiObjectResponseBuilder<T>.SetException(Exception exception)
        {
            throw new NotImplementedException();
        }

        IApiObjectResponseBuilder<T> IApiObjectResponseBuilder<T>.SetStatusCodeFailed(string code)
        {
            throw new NotImplementedException();
        }

        IApiObjectResponseBuilder<T> IApiObjectResponseBuilder<T>.SetStatusCodeSuccess(string code)
        {
            throw new NotImplementedException();
        }
    }
}
