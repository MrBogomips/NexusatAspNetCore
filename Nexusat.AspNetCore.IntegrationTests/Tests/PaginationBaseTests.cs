using Newtonsoft.Json.Linq;
using Nexusat.AspNetCore.IntegrationTests.Models;
using Xunit.Abstractions;

namespace Nexusat.AspNetCore.IntegrationTests.Tests;

public abstract class PaginationBaseTests<TSetup>: BaseTests<TSetup> where TSetup: class
{
    public PaginationBaseTests(ITestOutputHelper outputHelper) :base(outputHelper)
    {
    }

    protected static PaginationCursor ExtractPaginationCursor(JToken json) => 
        json.SelectToken("data").ToObject<PaginationCursor>();

    protected static PaginationInfo ExtractPaginationInfo(JToken json) =>
        json.SelectToken("navigation").ToObject<PaginationInfo>();
}