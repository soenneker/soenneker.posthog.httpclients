using Soenneker.PostHog.HttpClients.Abstract;
using Soenneker.TestHosts.Unit;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.PostHog.HttpClients.Tests;

[ClassDataSource<UnitTestHost>(Shared = SharedType.PerTestSession)]
public sealed class PostHogOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPostHogOpenApiHttpClient _httpclient;

    public PostHogOpenApiHttpClientTests(UnitTestHost host) : base(host)
    {
        _httpclient = Resolve<IPostHogOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
