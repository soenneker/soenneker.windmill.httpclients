using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Windmill.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Windmill.HttpClients.Registrars;

/// <summary>
/// Registers the Windmill API HTTP client provider.
/// </summary>
public static class WindmillOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds the Windmill HTTP client provider as a singleton service.
    /// </summary>
    public static IServiceCollection AddWindmillOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IWindmillOpenApiHttpClient, WindmillOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds the Windmill HTTP client provider as a scoped service.
    /// </summary>
    public static IServiceCollection AddWindmillOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IWindmillOpenApiHttpClient, WindmillOpenApiHttpClient>();

        return services;
    }
}
