using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Windmill.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Windmill.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class WindmillOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="WindmillOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddWindmillOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IWindmillOpenApiHttpClient, WindmillOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="WindmillOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddWindmillOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IWindmillOpenApiHttpClient, WindmillOpenApiHttpClient>();

        return services;
    }
}
