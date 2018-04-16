using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTests.Models;
using Nexusat.AspNetCore.IntegrationTestsFakeService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests
{
    public abstract class BaseTests<TSetup> where TSetup: class
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }
        protected ITestOutputHelper Output { get; }

        public BaseTests(ITestOutputHelper outputHelper)
        {
            Output = outputHelper;
            Server = new TestServer(
                new WebHostBuilder()
                .UseStartup<TSetup>());

            Client = Server.CreateClient();
        }

        protected static Status ExtractStatus(JObject json)
        {
            return
                new Status
                {
                    HttpCode = json.SelectToken("status.httpCode").Value<int>(),
                    Code = json.SelectToken("status.code").Value<string>(),
                    Description = json.SelectToken("status.description")?.Value<string>(),
                    UserDescription = json.SelectToken("status.userDescription")?.Value<string>()
                };
        }

        protected static ExceptionInfo ExtractExceptionInfo(JObject json)
        {
            return
                new ExceptionInfo {
                    Type = json.SelectToken("exception.type").Value<string>(),
                    Message = json.SelectToken("exception.message").Value<string>(),
                    StackTrace = json.SelectToken("exception.stackTrace")?.Values<string>()
                };
        }

        protected static T ExtractObjectData<T>(JObject json) => json.SelectToken("data").Value<T>();
        protected static IEnumerable<T> ExtractEnumData<T>(JObject json) => json.SelectToken("data").Values<T>();


        protected async static Task<string> ReadAsStringAsync(HttpContent content)
            => await content.ReadAsStringAsync();

        protected async static Task<JObject> ReadAsJObjectAsync(HttpContent content)
            => JObject.Parse(await ReadAsStringAsync(content));
    }
}
