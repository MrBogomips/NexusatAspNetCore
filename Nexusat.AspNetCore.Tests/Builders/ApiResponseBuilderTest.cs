using System;
using Nexusat.AspNetCore.Builders;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Implementations;
using Nexusat.AspNetCore.Models;
using Xunit;

namespace Nexusat.AspNetCore.Tests.Builders
{
    public class ApiResponseBuilderTest
    {
        public ApiResponseBuilderTest()
        {
        }

        [Fact]
        public void ApiResponseBuilderVoidConstructorTest() {
            // Act
            IApiResponseBuilder builder = new ApiResponseBuilder();
            IApiResponse response = builder.GetResponse();

            // Assert
            // A new ApiResponse object is provided by the builder
            Assert.NotNull(response);
        }

        [Fact]
        public void ApiResponseBuilderNonVoidConstructorTest()
        {
            // Act
            IApiResponse expected = new ApiResponse();
            IApiResponseBuilder builder = new ApiResponseBuilder(expected);
            IApiResponse actual = builder.GetResponse();

            // Assert
            // The builder will return the original instance
            Assert.Same(expected, actual);
        }

        [Theory]
        [InlineData(200, "OK_200", "Desc200", "UserDesc200")]
        [InlineData(299, "KO_299", "mDesc299", "mmUserDesc299")]
        public void ApiResponseBuilderBuildingTest(int httpCode, string code, string description, string userDescription) {
            // Act
            IApiResponseBuilder builder = new ApiResponseBuilder();
            IApiResponse response = builder.SetHttpCode(httpCode)
                   .SetStatusCode(code)
                   .SetDescription(description)
                   .SetUserDescription(userDescription)
                   .GetResponse();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(httpCode, response.Status.HttpCode);
            Assert.Equal(code, response.Status.Code);
            Assert.Equal(description, response.Status.Description);
            Assert.Equal(userDescription, response.Status.UserDescription);
        }

        [Fact]
        public void ApiResponseBuilderInvalidStateTest()
        {
            // Act
            IApiResponseBuilder builder = new ApiResponseBuilder();
            IApiResponse response = builder
                .SetHttpCode(100)
                .SetHttpCode(120)
                .GetResponse();

            // Assert
            Assert.Throws<BuilderInvalidStateException>(() => builder.SetHttpCode(100));
        }
    }
}
