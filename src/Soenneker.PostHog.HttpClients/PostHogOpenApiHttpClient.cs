using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.PostHog.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.PostHog.HttpClients;

///<inheritdoc cref="IPostHogOpenApiHttpClient"/>
public sealed class PostHogOpenApiHttpClient : IPostHogOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    private const string _prodBaseUrl = "https://app.posthog.com";

    public PostHogOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(PostHogOpenApiHttpClient), (config: _config, baseUrl: _config["PostHog:ClientBaseUrl"] ?? _prodBaseUrl),
            static state =>
            {
                var apiKey = state.config.GetValueStrict<string>("PostHog:ApiKey");
                string authHeaderName = state.config["PostHog:AuthHeaderName"] ?? "Authorization";
                string authHeaderValueTemplate = state.config["PostHog:AuthHeaderValueTemplate"] ?? "Bearer {token}";
                string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

                return new HttpClientOptions
                {
                    BaseAddress = new Uri(state.baseUrl),
                    DefaultRequestHeaders = new Dictionary<string, string>
                    {
                        { authHeaderName, authHeaderValue },
                    }
                };
            }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(nameof(PostHogOpenApiHttpClient));
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(nameof(PostHogOpenApiHttpClient));
    }
}