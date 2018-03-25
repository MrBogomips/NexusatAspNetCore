using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTestsFakeService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{
    public abstract class BaseTests
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }
        protected ITestOutputHelper Output { get; }

        public BaseTests(ITestOutputHelper outputHelper)
        {
            Output = outputHelper;
            Server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>());

            Client = Server.CreateClient();
        }

        protected async static Task<string> ReadAsStringAsync(HttpContent content)
            => await content.ReadAsStringAsync();

        protected async static Task<JObject> ReadAsJObjectAsync(HttpContent content)
            => JObject.Parse(await ReadAsStringAsync(content));
    }
}
