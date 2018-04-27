using System;
using System.Linq;
using System.Net;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTests.Models;
using Nexusat.AspNetCore.IntegrationTestsFakeService;
using Xunit;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{

    public class AcceptedControllerTests : BaseTests<StartupConfigurationOne>
    {
        public AcceptedControllerTests(ITestOutputHelper output
            ) : base(output) { }

        [Fact]
        public async void AcceptedApiResponseUri()
        {
            // Act
            var response = await Client.PostAsync("/Accepted/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
                Code = "OK_TEST_DEFAULT",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Accepted/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }

        [Fact]
        public async void AcceptedApiResponseObjectUri()
        {
            // Act
            var response = await Client.PostAsync("/Accepted/object/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
                Code = "OK_TEST_DEFAULT",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Accepted/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("data: 666", json.SelectToken("data"));
        }

        [Fact]
        public async void AcceptedAtActionFake()
        {
            // Act
            var response = await Client.PostAsync("/Accepted/action/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
                Code = "OK_TEST_DEFAULT",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Accepted/index/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }

        [Fact]
        public async void AcceptedAtRouteFake()
        {
            // Act
            var response = await Client.PostAsync("/Accepted/route/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
                Code = "OK_TEST_DEFAULT",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Accepted/index/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }
    }
}
