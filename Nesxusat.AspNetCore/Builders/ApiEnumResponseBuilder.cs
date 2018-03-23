using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Implementations;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal class ApiEnumResponseBuilder<T> : ApiResponseBuilderBase, IApiEnumResponseBuilder<T>
    {

        private IApiEnumResponse<T> Response => _response as IApiEnumResponse<T>;

        public ApiEnumResponseBuilder(): base(new ApiEnumResponse<T>()) { }

        public IApiEnumResponse<T> Build()
        {
            CheckBuildStateForFinalBuild();
            return Response;
        }

        public IApiEnumResponseBuilder<T> SetData(IEnumerable<T> data)
        {
            CheckBuildStateWhileBuilding();
            Response.Data = data;
            return this;
        }

        public IApiEnumResponseBuilder<T> SetHttpCode(int code)
        {
            InternalSetHttpCode(code);
            return this;
        }

        public IApiEnumResponseBuilder<T> SetException(Exception exception)
        {
            InternalSetException(exception);
            return this;
        }

        public IApiEnumResponseBuilder<T> SetStatusCodeFailed(string code)
        {
            InternalSetStatusCodeFailed(code);
            return this;
        }

        public IApiEnumResponseBuilder<T> SetStatusCodeSuccess(string code)
        {
            InternalSetStatusCodeSuccess(code);
            return this;
        }
    }
}
