using Microsoft.EntityFrameworkCore;

namespace Boba.Settings.EntityFrameworkCore;

/// <summary>
/// Represents a repository for managing settings in the application context.
/// </summary>
public partial class BobaSettingRepository(IBobaApplicationDbContext context) : IBobaSettingRepository
{
    private readonly IBobaApplicationDbContext _context = context;

    /// <summary>
    /// Deletes the specified setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task DeleteAsync(BobaSetting setting)
    {
        _context.Settings.Remove(setting);

        await _context.SaveChangesAsync(default);
    }

    /// <summary>
    /// Deletes the specified list of settings asynchronously.
    /// </summary>
    /// <param name="settings">The list of settings to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(IList<BobaSetting> settings)
    {
        _context.Settings.RemoveRange(settings);

        await _context.SaveChangesAsync(default);
    }

    /// <summary>
    /// Deletes the setting with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the setting to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">Thrown when no setting with the specified ID is found.</exception>
    public virtual async Task DeleteByIdAsync(int id)
    {
        var setting = await GetByIdAsync(id);

        if (setting == null)
            throw new ArgumentException(nameof(id));

        await DeleteAsync(setting);
    }

    /// <summary>
    /// Retrieves all settings asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns a list of all settings.</returns>
    public virtual async Task<IList<BobaSetting>> GetAllAsync()
    {
        return await _context.Settings.ToListAsync();
    }

    /// <summary>
    /// Retrieves a setting by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the setting to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the setting with the specified ID, or null if not found.</returns>
    public virtual async Task<BobaSetting> GetByIdAsync(int id)
    {
        return await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Retrieves settings by their IDs asynchronously.
    /// </summary>
    /// <param name="ids">The list of IDs of the settings to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of settings with the specified IDs.</returns>
    public virtual async Task<IList<BobaSetting>> GetByIdsAsync(IList<int> ids)
    {
        return await _context.Settings.Where(s => ids.Any(id => id == s.Id)).ToListAsync();
    }

    /// <summary>
    /// Retrieves settings by their key asynchronously.
    /// </summary>
    /// <param name="name">The key of the settings to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of settings with the specified key.</returns>
    public virtual async Task<IList<BobaSetting>> GetByKeyAsync(string name)
    {
        return await _context.Settings.Where(s => s.Name == name).ToListAsync();
    }

    /// <summary>
    /// Retrieves settings by their value asynchronously.
    /// </summary>
    /// <param name="value">The value of the settings to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of settings with the specified value.</returns>
    public virtual async Task<IList<BobaSetting>> GetByValueAsync(string value)
    {
        return await _context.Settings.Where(s => s.Value == value).ToListAsync();
    }

    /// <summary>
    /// Inserts a new setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to insert.</param>
    /// <returns>A task representing the asynchronous operation that returns the inserted setting.</returns>
    public virtual async Task<BobaSetting> InsertAsync(BobaSetting setting)
    {
        await _context.Settings.AddAsync(setting);
        await _context.SaveChangesAsync(default);

        return setting;
    }

    /// <summary>
    /// Inserts a bulk of settings asynchronously.
    /// </summary>
    /// <param name="settings">The list of settings to insert.</param>
    /// <returns>A task representing the asynchronous operation that returns the list of inserted settings.</returns>
    public virtual async Task<IList<BobaSetting>> InsertBulkAsync(List<BobaSetting> settings)
    {
        await _context.Settings.AddRangeAsync(settings);
        await _context.SaveChangesAsync(default);

        return settings;
    }

    /// <summary>
    /// Updates an existing setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to update.</param>
    /// <returns>A task representing the asynchronous operation that returns the updated setting.</returns>
    public virtual async Task<BobaSetting> UpdateAsync(BobaSetting setting)
    {
        _context.Settings.Update(setting);
        await _context.SaveChangesAsync(default);

        return setting;
    }
}
