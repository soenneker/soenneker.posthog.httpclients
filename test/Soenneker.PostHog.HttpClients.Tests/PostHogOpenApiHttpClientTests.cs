using Soenneker.PostHog.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.PostHog.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PostHogOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPostHogOpenApiHttpClient _httpclient;

    public PostHogOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IPostHogOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
