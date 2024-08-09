using Microsoft.EntityFrameworkCore;

namespace Boba.Settings.EntityFrameworkCore;

public interface IBobaApplicationDbContext
{
    DbSet<BobaSetting> Settings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
