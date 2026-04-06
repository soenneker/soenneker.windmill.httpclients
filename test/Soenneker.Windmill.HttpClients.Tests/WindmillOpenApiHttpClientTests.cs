using Soenneker.Windmill.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Windmill.HttpClients.Tests;

[Collection("Collection")]
public sealed class WindmillOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IWindmillOpenApiHttpClient _httpclient;

    public WindmillOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IWindmillOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
