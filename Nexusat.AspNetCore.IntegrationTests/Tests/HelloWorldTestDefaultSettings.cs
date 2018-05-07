using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Nexusat.AspNetCore.IntegrationTestsFakeService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{
    public class HelloWorldDefaultSettingsTest: BaseTests<StartupConfigurationDefault>
    {

        public HelloWorldDefaultSettingsTest(ITestOutputHelper output
            ) : base(output) { }
        /// <summary>
        /// Just to test Integration wiring
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var response = await Client.GetAsync("/hello_world");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal("Hello World!", responseString);
        }

        [Fact]
        public async Task ReturnHelloWorldResponse()
        {
            // Act
            var response = await Client.GetAsync("/hello_world/api_response");
            response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();


            // Assert
            //Assert.Equal("Hello World!", responseString);
        }
    }
}
