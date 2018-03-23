using System;
using System.Collections.Generic;
using System.Text;
using Nesxusat.AspNetCore.Models;

namespace Nesxusat.AspNetCore.Builders
{
    internal class ApiObjectResponseBuilder<T> : ApiResponseBuilderBase, IApiObjectResponseBuilder<T>
    {
        public ApiResponseBuilderBase SetData(T data)
        {
            throw new NotImplementedException();
        }

        public IApiObjectResponse<T> Build()
        {
            throw new NotImplementedException();
        }
    }
}
