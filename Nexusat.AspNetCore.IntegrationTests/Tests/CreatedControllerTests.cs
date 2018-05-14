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

    public class CreatedControllerTests : BaseTests<StartupConfigurationOne>
    {
        public CreatedControllerTests(ITestOutputHelper output
            ) : base(output) { }

        [Fact]
        public async void CreatedApiResponseUri()
        {
            // Act
            var response = await Client.PostAsync("/Created/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
				Code = "OK_CREATED",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Created/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }

        [Fact]
        public async void CreatedApiResponseObjectUri()
        {
            // Act
            var response = await Client.PostAsync("/Created/object/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
                Code = "OK_CREATED",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Created/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Equal("data: 666", json.SelectToken("data"));
        }

        [Fact]
        public async void CreatedAtActionFake()
        {
            // Act
            var response = await Client.PostAsync("/Created/action/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
				Code = "OK_CREATED",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Created/index/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }

        [Fact]
        public async void CreatedAtRouteFake()
        {
            // Act
            var response = await Client.PostAsync("/Created/route/666", null);
            response.EnsureSuccessStatusCode();
            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

            var actualStatus = ExtractStatus(json);
            var expectedStatus = new Status
            {
                HttpCode = 201,
				Code = "OK_CREATED",
                Description = null,
                UserDescription = null
            };

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpCode);
            Assert.Equal("/Created/index/666", location);
            Assert.Equal(expectedStatus, actualStatus);
            Assert.Null(json.SelectToken("data"));
        }
    }
}
