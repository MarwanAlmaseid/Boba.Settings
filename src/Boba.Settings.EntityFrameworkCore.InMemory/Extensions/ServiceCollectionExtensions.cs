﻿using Boba.Settings.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

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
    const string defaultDBName = "BobaSettingsDB";

    public static IServiceCollection UseInMemory(this IServiceCollection services, string? dbName = defaultDBName)
    {
        services.UseEFCore();

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseInMemoryDatabase(dbName ?? defaultDBName);
        });

        return services;
    }
}
