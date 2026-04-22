using Soenneker.Windmill.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Windmill.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class WindmillOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IWindmillOpenApiHttpClient _httpclient;

    public WindmillOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IWindmillOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
