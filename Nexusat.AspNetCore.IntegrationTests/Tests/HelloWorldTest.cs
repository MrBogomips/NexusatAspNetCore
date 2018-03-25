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

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{
    public class HelloWorldTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HelloWorldTest()
        {

            _server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();
                
        }

        [Fact]
        public async Task ReturnHelloWorld()
        {
            // Act
            var response = await _client.GetAsync("/hello_world");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal("Hello World!", responseString);
        }
    }
}
