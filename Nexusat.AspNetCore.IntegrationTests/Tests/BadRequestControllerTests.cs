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
			var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var httpCode = response.StatusCode;
			var actualStatus = ExtractStatus(json);
                     
            // Assert         
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, httpCode);
			Assert.Equal("KO_UNSUPPORTED_MEDIA_TYPE", actualStatus.Code);
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
			var json = await ReadAsJObjectAsync(response.Content);

			Output.WriteLine(json.ToString());

			var httpCode = response.StatusCode;
			var actualStatus = ExtractStatus(json);
			var actualErrors = ExtractValidationErrorsInfo(json);

			// Assert         
			Assert.Equal(HttpStatusCode.BadRequest, httpCode);
			Assert.Equal("KO_BAD_REQUEST", actualStatus.Code);
			Assert.NotNull(actualErrors);
			Assert.True(actualErrors.Count > 0);
        }

		[Fact]
		public async void ModelStateAutoValidation()
        {
            // Setup
            var request = GetJsonContent(new
            {
                Name = "G",
                Surname = "Costagliola"
            });

            // Act
			var response = await Client.PostAsync("/BadRequest/ModelStateAutoValidation", request);
            var json = await ReadAsJObjectAsync(response.Content);

            Output.WriteLine(json.ToString());

            var httpCode = response.StatusCode;
            var actualStatus = ExtractStatus(json);
            var actualErrors = ExtractValidationErrorsInfo(json);

            // Assert         
            Assert.Equal(HttpStatusCode.BadRequest, httpCode);
            Assert.Equal("KO_BAD_REQUEST", actualStatus.Code);
            Assert.NotNull(actualErrors);
            Assert.True(actualErrors.Count > 0);
        }
    }
}
