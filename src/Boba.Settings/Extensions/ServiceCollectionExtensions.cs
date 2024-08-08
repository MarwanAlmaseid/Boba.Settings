using Microsoft.Extensions.DependencyInjection;

namespace Boba.Settings;

/// <summary>
/// Provides extension methods for configuring services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Boba settings and related services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with added services.</returns>
    public static IServiceCollection AddBobaSettings(this IServiceCollection services)
    {
        services.AddScoped<IBobaSettingService, BobaSettingService>();

        var appDomain = new AppDomainTypeFinder();
        var settings = appDomain.FindClassesOfType(typeof(IBobaSettings), true).ToList();

        foreach (var setting in settings)
        {
            services.AddScoped(
                setting,
                serviceProvider =>
                {
                    return serviceProvider
                        .GetRequiredService<IBobaSettingService>()
                        .LoadSettingAsync(setting)
                        .Result;
                }
            );
        }

        return services;
    }
}
