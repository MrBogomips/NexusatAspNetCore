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

    public class BadRequestControllerTests : BaseTests<StartupConfigurationOne>
    {
		public BadRequestControllerTests(ITestOutputHelper output
            ) : base(output) { }

		[Fact]
		public async void EmptyRequestMustCauseBadRequest()
		{         
			// Act
			var response = await Client.PostAsync("/BadRequest/ModelStateManualValidation", null);
			//response.EnsureSuccessStatusCode();
			Output.WriteLine(await response.Content.ReadAsStringAsync());
			return;

			var json = await ReadAsJObjectAsync(response.Content);
			var httpCode = response.StatusCode;
			//var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

			Output.WriteLine(json.ToString());

		}
        [Fact]
		public async void ModelStateManualValidation()
        {
			// Setup
			var request = GetJsonContent(new { 
				Name = "G",
                Surname = "Costagliola"
			});
            
            // Act
			var response = await Client.PostAsync("/BadRequest/ModelStateManualValidation", request);
            //response.EnsureSuccessStatusCode();
			Output.WriteLine(await response.Content.ReadAsStringAsync());
			return;

            var json = await ReadAsJObjectAsync(response.Content);
            var httpCode = response.StatusCode;
            //var location = response.Headers.GetValues(HeaderNames.Location).FirstOrDefault();

            Output.WriteLine(json.ToString());

			return;
            /*
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
            */
        }
    }
}
