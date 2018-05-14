using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTests.Models;
using Nexusat.AspNetCore.IntegrationTestsFakeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{
    public class ApiResponseControllerTests : BaseTests<StartupConfigurationOne>
    {
        public ApiResponseControllerTests(ITestOutputHelper output
            ) : base(output) { }

        [Fact]
        public async void ApiResponse200()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/200");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var statusCode = json.SelectToken("status.code").Value<string>();
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // HTTP200
            Assert.Equal("OK", statusCode);
        }



        [Fact]
        public async void ApiResponse299CustomResponse()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/299CustomRespone");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());
           
            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 299,
                Code = "OK_299_CUSTOM",
                Description = "Description",
                UserDescription = "UserDescription"
            };

            // Assert
            Assert.Equal("299", response.StatusCode.ToString()); // HTTP200
            Assert.Equal(expectedStatus, actualStatus);
        }

        [Fact]
        public async void ApiResponse299CustomStringResponse()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/299CustomStringRespone");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var actualData = ExtractObjectData<string>(json);
            var expectedStatus = new Status
            {
                HttpCode = 299,
                Code = "OK_299_CUSTOM",
                Description = "Description",
                UserDescription = "UserDescription"
            };

            // Assert
            Assert.Equal("299", response.StatusCode.ToString()); // HTTP200
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("Hello World!", actualData);
        }

        [Fact]
        public async void ApiResponse299CustomEnumStringResponse()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/299CustomEnumStringRespone");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var actualData = ExtractEnumData<string>(json);
            var expectedStatus = new Status
            {
                HttpCode = 299,
                Code = "OK_299_CUSTOM",
                Description = "Description",
                UserDescription = "UserDescription"
            };

            // Assert
            Assert.Equal("299", response.StatusCode.ToString()); // HTTP200
            Assert.Equal(expectedStatus, actualStatus);

            // Check enum equality by mutual inclusion
            var expectedData = new List<string> { "Hello", "World" };
            Assert.Equal(expectedData, actualData);
        }

        [Fact]
        public async void ApiResponse200OkResponseWithException()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/200OkResponseWithException");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var actualException = ExtractExceptionInfo(json);
            var expectedStatus = new Status
            {
                HttpCode = 200,
                Code = "OK",
                Description = null,
                UserDescription = null
            };

            // Assert
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("System.Exception", actualException.Type);
            Assert.Equal("Fake exception", actualException.Message);
            Assert.NotEmpty(actualException.StackTrace);
        }

        [Fact]
        public async void ApiResponse500KoUnhandledException()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/500KoUnhandledException");
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var actualException = ExtractExceptionInfo(json);
            var expectedStatus = new Status
            {
                HttpCode = 500,
                Code = "KO_UNHANDLED_EXCEPTION",
                Description = null,
                UserDescription = null
            };

            // Assert
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("System.DivideByZeroException", actualException.Type);
            Assert.Equal("Attempted to divide by zero.", actualException.Message);
            Assert.NotEmpty(actualException.StackTrace);
        }

        #region Ok (HTTP 200) Helper Methods flavours
        [Fact]
        public async void Api200OkResponseWithoutPayload() {
            // Act
            var response = await Client.GetAsync("/ApiResponse/200OkResponseWithoutPayload");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 200,
                Code = "OK",
                Description = null,
                UserDescription = null
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK /* 200 */, httpCode);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }

        [Fact]
        public async void Api200OkResponseWithObject()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/200OkResponseWithObject");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 200,
                Code = "OK",
                Description = null,
                UserDescription = null
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK /* 200 */, httpCode);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("Ciccio", json.SelectToken("data").Value<string>());
        }

        [Fact]
        public async void Api200OkResponseWithManyObjects()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/200OkResponseWithManyObjects");
            //response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 200,
                Code = "OK_UNK",
                Description = null,
                UserDescription = null
            };

            // Assert
            Assert.Equal(HttpStatusCode.OK /* 200 */, httpCode);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal(new[] { "Ciccio", "buffo" }, json.SelectToken("data").Values<string>());
        }
        #endregion Ok (HTTP 200) Helper Methods flavours

       
        #region Accpepted (HTTP 202) Helper Methods flavours
        [Fact]
        public async void ApiGetAcceptedResponseWithoutPayload()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/202OkResponseWithoutPayload");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 202,
                Code = "OK_ACCEPTED",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, httpCode);
			Assert.Equal("http://www.google.com/", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }

        [Fact]
        public async void ApiGetAcceptedResponseWithObject()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/202OkResponseWithObject");
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 202,
                Code = "OK_ACCEPTED",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Accepted, httpCode);
			Assert.Equal("http://www.google.com/", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("payload", json.SelectToken("data").Value<string>());
        }

        #endregion Accpepted (HTTP 202) Helper Methods flavours



        #region Middleware Global Exception Handling
        /// <summary>
        /// Requesting a route not found will generate a standard 404 response
        /// </summary>
        [Fact]
        public async void ApiNotFoundGlobalHandling()
        {
            // Act
            var response = await Client.GetAsync("/ApiResponse/SomeVeryUnlikeEndPoint");
            //response.EnsureSuccessStatusCode();
            //Output.WriteLine(await response.Content.ReadAsStringAsync());
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            //var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 404,
                Code = "KO_NOT_FOUND",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, httpCode);
            Assert.Equal(expectedStatus, actualStatus);
        }
        #endregion Middleware Global Exception Handling
       
    }
}
