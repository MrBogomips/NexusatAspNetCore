using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTests.Models;
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
    public class ApiResponseControllerTests : BaseTests
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
            Assert.Equal("OK_TEST_DEFAULT", statusCode);
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
       
    }
}
