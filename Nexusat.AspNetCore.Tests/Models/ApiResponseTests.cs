using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Mvc;
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

        [Fact]
        public void ApiResponseWithExceptionExtension() {
            // Setup
            IApiResponse apiResponse = new ApiResponse();
            IApiObjectResponse<string> objectResponse = new ApiObjectResponse<string>("Beatrice");
            IApiEnumResponse<string> enumResponse = new ApiEnumResponse<string>(new List<string> {"Nausicaa", "Beatrice"});

            var ex1 = new InvalidCastException("ExceptionMessage");

            // Act
            apiResponse.SetException(ex1);
            objectResponse.SetException(ex1);
            enumResponse.SetException(ex1);

            // Assert
            Assert.NotNull(apiResponse.Exception);
            Assert.NotNull(objectResponse.Exception);
            Assert.NotNull(enumResponse.Exception);

            Assert.Equal("ExceptionMessage", apiResponse.Exception.Message);
            Assert.Equal("ExceptionMessage", objectResponse.Exception.Message);
            Assert.Equal("ExceptionMessage", enumResponse.Exception.Message);
        }
    }
}
