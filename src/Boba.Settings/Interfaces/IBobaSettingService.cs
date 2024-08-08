using Boba.Settings.Models;
using System.Linq.Expressions;

namespace Boba.Settings;

public interface IBobaSettingService
{
    /// <summary>
    /// Retrieves all settings asynchronously.
    /// </summary>
    /// <returns>The list of all settings.</returns>
    Task<IList<BobaSetting>> GetAllSettingsAsync();

    /// <summary>
    /// Gets all registered settings asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of settings.</returns>
    Task<IList<BobaSettingDto>> GetAllRegisteredSettingsAsync();

    /// <summary>
    /// Retrieves a setting by its ID asynchronously.
    /// </summary>
    /// <param name="settingId">The ID of the setting to retrieve.</param>
    /// <returns>The setting with the specified ID.</returns>
    Task<BobaSetting> GetSettingByIdAsync(int settingId);

    /// <summary>
    /// Retrieves the setting value corresponding to the specified key asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the setting value.</typeparam>
    /// <param name="key">The key of the setting to retrieve.</param>
    /// <param name="defaultValue">The default value to return if the setting is not found.</param>
    /// <returns>
    /// The setting value corresponding to the specified key, or the default value if the key is not found or if it's empty.
    /// </returns>
    Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue = default!);

    /// <summary>
    /// Retrieves a setting asynchronously based on the specified key.
    /// </summary>
    /// <param name="key">The key of the setting to retrieve.</param>
    /// <returns>The setting corresponding to the specified key, or null if not found.</returns>
    Task<BobaSetting> GetSettingAsync(string key);

    /// <summary>
    /// Gets the key corresponding to the specified property in the settings.
    /// </summary>
    /// <typeparam name="TSettings">The type of settings.</typeparam>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property for which to get the key.</param>
    /// <returns>The key corresponding to the specified property.</returns>
    /// <exception cref="ArgumentException">Thrown when the expression does not refer to a property.</exception>
    string GetSettingKey<TSettings, T>(
        TSettings settings,
        Expression<Func<TSettings, T>> keySelector
    )
        where TSettings : IBobaSettings, new();

    /// <summary>
    /// Loads settings of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to load, must implement <see cref="IBobaSettings"/> and have a parameterless constructor.</typeparam>
    /// <returns>The loaded settings.</returns>
    Task<T> LoadSettingAsync<T>()
        where T : IBobaSettings, new();

    /// <summary>
    /// Loads settings of the specified type asynchronously.
    /// </summary>
    /// <param name="type">The type of settings to load.</param>
    /// <returns>The loaded settings.</returns>
    Task<IBobaSettings> LoadSettingAsync(Type type);

    /// <summary>
    /// Saves the specified settings asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to save, must implement <see cref="IBobaSettings"/> and have a parameterless constructor.</typeparam>
    /// <param name="settings">The settings to save.</param>
    /// <remarks>
    /// This method iterates through the properties of the specified settings object, converts their values to strings, and saves them as key-value pairs.
    /// </remarks>
    Task SaveSettingAsync<T>(T settings)
        where T : IBobaSettings, new();

    /// <summary>
    /// Saves the specified setting asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to save, must implement <see cref="IBobaSettings"/> and have a parameterless constructor.</typeparam>
    /// <typeparam name="TPropType">The type of the property to save.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property to save.</param>
    /// <exception cref="ArgumentException">Thrown when the expression does not refer to a property.</exception>
    Task SaveSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector)
        where T : IBobaSettings, new();

    /// <summary>
    /// Sets the specified setting asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the setting value.</typeparam>
    /// <param name="key">The key of the setting.</param>
    /// <param name="value">The value of the setting.</param>
    Task SetSettingAsync<T>(string key, T value);

    /// <summary>
    /// Inserts a new setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to insert.</param>
    Task InsertSettingAsync(BobaSetting setting);

    /// <summary>
    /// Updates an existing setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to update.</param>
    /// <exception cref="ArgumentNullException">Thrown when the setting is null.</exception>
    Task UpdateSettingAsync(BobaSetting setting);

    /// <summary>
    /// Deletes a setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to delete.</param>
    Task DeleteSettingAsync(BobaSetting setting);

    /// <summary>
    /// Deletes multiple settings asynchronously.
    /// </summary>
    /// <param name="settings">The list of settings to delete.</param>
    Task DeleteSettingsAsync(IList<BobaSetting> settings);

    /// <summary>
    /// Deletes all settings of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to delete, must implement <see cref="IBobaSettings"/> and have a parameterless constructor.</typeparam>
    /// <remarks>
    /// This method deletes all settings whose keys correspond to properties of the specified type.
    /// </remarks>
    Task DeleteSettingAsync<T>()
        where T : IBobaSettings, new();

    /// <summary>
    /// Deletes the specified setting asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to which the setting belongs, must implement <see cref="IBobaSettings"/> and have a parameterless constructor.</typeparam>
    /// <typeparam name="TPropType">The type of the property representing the setting.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property of the setting to delete.</param>
    Task DeleteSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector)
        where T : IBobaSettings, new();

    /// <summary>
    /// Checks if the specified setting exists asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to which the setting belongs, must implement <see cref="IBobaSettings"/> and have a parameterless constructor.</typeparam>
    /// <typeparam name="TPropType">The type of the property representing the setting.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property of the setting to check.</param>
    /// <returns>True if the setting exists; otherwise, false.</returns>
    Task<bool> SettingExistsAsync<T, TPropType>(
        T settings,
        Expression<Func<T, TPropType>> keySelector
    )
        where T : IBobaSettings, new();
}
