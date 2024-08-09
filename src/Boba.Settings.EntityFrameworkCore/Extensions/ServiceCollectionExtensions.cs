using Boba.Settings.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boba.Settings;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseEFCore(this IServiceCollection services)
    {
        // Add the application database context and configure it to use SQL Server
        services.AddScoped<IBobaApplicationDbContext>(provider =>
            provider.GetRequiredService<BobaApplicationDbContext>()
        );

        // Add setting repository
        services.AddScoped<IBobaSettingRepository, BobaSettingRepository>();

        return services;
    }
}
