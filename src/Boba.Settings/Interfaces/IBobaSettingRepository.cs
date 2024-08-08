namespace Boba.Settings;

public interface IBobaSettingRepository
{
    /// <summary>
    /// Retrieves all settings asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns a list of all settings.</returns>
    Task<IList<BobaSetting>> GetAllAsync();

    /// <summary>
    /// Retrieves a setting by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the setting to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the setting with the specified ID, or null if not found.</returns>
    Task<BobaSetting> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves settings by their IDs asynchronously.
    /// </summary>
    /// <param name="ids">The list of IDs of the settings to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of settings with the specified IDs.</returns>
    Task<IList<BobaSetting>> GetByIdsAsync(IList<int> ids);

    /// <summary>
    /// Retrieves settings by their key asynchronously.
    /// </summary>
    /// <param name="name">The key of the settings to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of settings with the specified key.</returns>
    Task<IList<BobaSetting>> GetByKeyAsync(string key);

    /// <summary>
    /// Retrieves settings by their value asynchronously.
    /// </summary>
    /// <param name="value">The value of the settings to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns a list of settings with the specified value.</returns>
    Task<IList<BobaSetting>> GetByValueAsync(string value);

    /// <summary>
    /// Inserts a new setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to insert.</param>
    /// <returns>A task representing the asynchronous operation that returns the inserted setting.</returns>
    Task<BobaSetting> InsertAsync(BobaSetting setting);

    /// <summary>
    /// Inserts a bulk of settings asynchronously.
    /// </summary>
    /// <param name="settings">The list of settings to insert.</param>
    /// <returns>A task representing the asynchronous operation that returns the list of inserted settings.</returns>
    Task<IList<BobaSetting>> InsertBulkAsync(List<BobaSetting> settings);

    /// <summary>
    /// Updates an existing setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to update.</param>
    /// <returns>A task representing the asynchronous operation that returns the updated setting.</returns>
    Task<BobaSetting> UpdateAsync(BobaSetting setting);

    /// <summary>
    /// Deletes the specified setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(BobaSetting setting);

    /// <summary>
    /// Deletes the specified list of settings asynchronously.
    /// </summary>
    /// <param name="settings">The list of settings to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(IList<BobaSetting> settings);

    /// <summary>
    /// Deletes the setting with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the setting to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">Thrown when no setting with the specified ID is found.</exception>
    Task DeleteByIdAsync(int id);
}
