[![](https://img.shields.io/nuget/v/soenneker.posthog.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.posthog.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.posthog.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.posthog.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.posthog.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.posthog.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.posthog.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.posthog.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.PostHog.HttpClients

Provides a cached `HttpClient` authenticated for PostHog's projects, organizations, insights, feature flags, experiments, dashboards, and other management APIs.

## Installation

```bash
dotnet add package Soenneker.PostHog.HttpClients
```

## Configuration

```json
{
  "PostHog": {
    "ApiKey": "your-personal-api-key",
    "ClientBaseUrl": "https://us.posthog.com/"
  }
}
```

Use the host for the account's region, such as `https://us.posthog.com/` or `https://eu.posthog.com/`, or the root URL of a self-hosted deployment. The key must be a personal API key with the scopes required by the endpoints you call; a project API key used for event ingestion is not a substitute.

## Usage

```csharp
using Soenneker.PostHog.HttpClients.Abstract;
using Soenneker.PostHog.HttpClients.Registrars;

services.AddPostHogOpenApiHttpClientAsSingleton();

IPostHogOpenApiHttpClient postHog = serviceProvider
    .GetRequiredService<IPostHogOpenApiHttpClient>();

HttpClient client = await postHog.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync(
    "api/projects/",
    cancellationToken);
response.EnsureSuccessStatusCode();
```

The provider owns the cached client. Scoped provider registrations use separate cache entries, so disposing one scope does not invalidate another scope's client.
