using Boba.Settings.EntityFrameworkCore;
using Boba.Settings.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Boba.Settings;

/// <summary>
/// Configures the services to use SQL Server as the database provider.
/// </summary>
/// <param name="services">The <see cref="IServiceCollection"/> to configure the services on.</param>
/// <param name="connectionString">The connection string to the SQL Server database.</param>
/// <returns>The modified <see cref="IServiceCollection"/>.</returns>
/// <exception cref="ArgumentNullException">Thrown when the connection string is null.</exception>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseSqlServer(this IServiceCollection services, [NotNull] string connectionString)
    {
        if (connectionString is null)
        {
            throw new ArgumentNullException("connectionString");
        }

        services.UseEFCore();

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        // Install SQL Server specific objects
        SqlServerObjectsInstaller.Install(connectionString);

        return services;
    }
}
