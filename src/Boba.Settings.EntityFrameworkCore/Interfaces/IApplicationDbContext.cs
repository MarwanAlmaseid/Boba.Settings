using Microsoft.EntityFrameworkCore;

namespace Boba.Settings.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Setting> Settings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
