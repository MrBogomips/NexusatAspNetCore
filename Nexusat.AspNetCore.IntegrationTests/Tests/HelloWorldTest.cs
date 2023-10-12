using Nexusat.AspNetCore.IntegrationTestsFakeService;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests;

public class HelloWorldTest: BaseTests<StartupConfigurationOne>
{

    public HelloWorldTest(ITestOutputHelper output
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
}