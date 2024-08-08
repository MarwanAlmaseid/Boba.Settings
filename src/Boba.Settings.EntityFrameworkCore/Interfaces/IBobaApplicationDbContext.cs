using Microsoft.EntityFrameworkCore;

namespace Boba.Settings.EntityFrameworkCore;

public interface IBobaApplicationDbContext
{
    DbSet<Setting> Settings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
