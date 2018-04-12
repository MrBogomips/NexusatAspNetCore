using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexusat.AspNetCore.Tests.Models
{
    public class ApiResponseTests
    {
        /// <summary>
        /// Internal interfaces are supposed to be implemented by all the kinds of
        /// Responses
        /// </summary>
        [Fact]
        public void ApiResponsesImplementsInternalInterfaces()
        {
            var apiResponse = new ApiResponse();
            var apiObjectResponse = new ApiObjectResponse<object>();
            var apiEnumResponse = new ApiEnumResponse<object>();

            Assert.IsAssignableFrom<IApiResponseInternal>(apiResponse);
            Assert.IsAssignableFrom<IApiResponseInternal>(apiObjectResponse);
            Assert.IsAssignableFrom<IApiResponseInternal>(apiEnumResponse);
        }
    }
}
