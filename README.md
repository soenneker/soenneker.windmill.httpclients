[![](https://img.shields.io/nuget/v/soenneker.windmill.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.windmill.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.windmill.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.windmill.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.windmill.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.windmill.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.windmill.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.windmill.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Windmill.HttpClients

Provides a cached `HttpClient` configured with a Windmill instance URL and bearer token.

## Installation

```bash
dotnet add package Soenneker.Windmill.HttpClients
```

## Configuration

```json
{
  "Windmill": {
    "Token": "your-user-token",
    "ClientBaseUrl": "https://app.windmill.dev/api/"
  }
}
```

Use your own instance URL for self-hosted Windmill. Keep the trailing slash after `api/` so relative workspace paths resolve correctly. `Windmill:ApiKey` remains supported as a legacy alias for `Token`.

## Registration and usage

```csharp
using Soenneker.Windmill.HttpClients.Abstract;
using Soenneker.Windmill.HttpClients.Registrars;

services.AddWindmillOpenApiHttpClientAsSingleton();

public sealed class WindmillService
{
    private readonly IWindmillOpenApiHttpClient _clientProvider;

    public WindmillService(IWindmillOpenApiHttpClient clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task<string> ListScripts(
        string workspace,
        CancellationToken cancellationToken)
    {
        HttpClient client = await _clientProvider.Get(cancellationToken);
        return await client.GetStringAsync(
            $"w/{Uri.EscapeDataString(workspace)}/scripts/list",
            cancellationToken);
    }
}
```

Use `AddWindmillOpenApiHttpClientAsScoped()` when the provider should follow a scope. Each provider owns its cached client and removes it when disposed.
