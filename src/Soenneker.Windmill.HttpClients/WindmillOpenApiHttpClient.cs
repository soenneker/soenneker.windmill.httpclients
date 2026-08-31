using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Windmill.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Windmill.HttpClients;

///<inheritdoc cref="IWindmillOpenApiHttpClient"/>
public sealed class WindmillOpenApiHttpClient : IWindmillOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _cacheKey = $"{nameof(WindmillOpenApiHttpClient)}-{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://app.windmill.dev/api/";

    public WindmillOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_cacheKey, (config: _config, baseUrl: _config["Windmill:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            string token = state.config["Windmill:Token"] ?? state.config.GetValueStrict<string>("Windmill:ApiKey");
            string authHeaderName = state.config["Windmill:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Windmill:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", token, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_cacheKey);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_cacheKey);
    }
}
