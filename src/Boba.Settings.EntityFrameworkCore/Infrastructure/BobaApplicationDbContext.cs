using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Boba.Settings.EntityFrameworkCore;

public class BobaApplicationDbContext : DbContext, IBobaApplicationDbContext
{
    public BobaApplicationDbContext(DbContextOptions<BobaApplicationDbContext> options)
        : base(options) { }

    public DbSet<Setting> Settings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
