using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Boba.Settings;

public class SettingService(ISettingRepository settingRepository) : ISettingService
{
    private readonly ISettingRepository _settingRepository = settingRepository;

    /// <summary>
    /// Retrieves all settings asynchronously.
    /// </summary>
    /// <returns>The list of all settings.</returns>
    public virtual async Task<IList<Setting>> GetAllSettingsAsync()
    {
        var settings = await _settingRepository.GetAllAsync();

        return settings;
    }

    /// <summary>
    /// Retrieves a setting by its ID asynchronously.
    /// </summary>
    /// <param name="settingId">The ID of the setting to retrieve.</param>
    /// <returns>The setting with the specified ID.</returns>
    public virtual async Task<Setting> GetSettingByIdAsync(int settingId)
    {
        return await _settingRepository.GetByIdAsync(settingId);
    }

    /// <summary>
    /// Retrieves the setting value corresponding to the specified key asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the setting value.</typeparam>
    /// <param name="key">The key of the setting to retrieve.</param>
    /// <param name="defaultValue">The default value to return if the setting is not found.</param>
    /// <returns>
    /// The setting value corresponding to the specified key, or the default value if the key is not found or if it's empty.
    /// </returns>
    public virtual async Task<T> GetSettingByKeyAsync<T>(string key, T defaultValue = default!)
    {
        if (string.IsNullOrEmpty(key))
            return defaultValue;

        var settings = await GetAllSettingsDictionaryAsync();
        key = key.Trim().ToLowerInvariant();
        if (!settings.ContainsKey(key))
            return defaultValue;

        var settingsByKey = settings[key];
        var setting = settingsByKey.FirstOrDefault();

        return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
    }

    /// <summary>
    /// Retrieves a setting asynchronously based on the specified key.
    /// </summary>
    /// <param name="key">The key of the setting to retrieve.</param>
    /// <returns>The setting corresponding to the specified key, or null if not found.</returns>
    public virtual async Task<Setting> GetSettingAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
            return null!;

        var settings = await GetAllSettingsDictionaryAsync();
        key = key.Trim().ToLowerInvariant();
        if (!settings.ContainsKey(key))
            return null!;

        var settingsByKey = settings[key];
        var setting = settingsByKey.FirstOrDefault();

        return setting is not null ? await GetSettingByIdAsync(setting.Id) : null!;
    }

    /// <summary>
    /// Gets the key corresponding to the specified property in the settings.
    /// </summary>
    /// <typeparam name="TSettings">The type of settings.</typeparam>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property for which to get the key.</param>
    /// <returns>The key corresponding to the specified property.</returns>
    /// <exception cref="ArgumentException">Thrown when the expression does not refer to a property.</exception>
    public virtual string GetSettingKey<TSettings, T>(TSettings settings, Expression<Func<TSettings, T>> keySelector) where TSettings : ISettings, new()
    {
        if (keySelector.Body is not MemberExpression member)
            throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

        if (member.Member is not PropertyInfo propInfo)
            throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

        var key = $"{typeof(TSettings).Name}.{propInfo.Name}";

        return key;
    }

    /// <summary>
    /// Loads settings of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to load, must implement <see cref="ISettings"/> and have a parameterless constructor.</typeparam>
    /// <returns>The loaded settings.</returns>
    public virtual async Task<T> LoadSettingAsync<T>() where T : ISettings, new()
    {
        return (T)await LoadSettingAsync(typeof(T));
    }

    /// <summary>
    /// Loads settings of the specified type asynchronously.
    /// </summary>
    /// <param name="type">The type of settings to load.</param>
    /// <returns>The loaded settings.</returns>
    public virtual async Task<ISettings> LoadSettingAsync(Type type)
    {
        var settings = Activator.CreateInstance(type);

        foreach (var prop in type.GetProperties())
        {
            // get properties we can read and write to
            if (!prop.CanRead || !prop.CanWrite)
                continue;

            var key = type.Name + "." + prop.Name;
            //load by store
            var setting = await GetSettingByKeyAsync<string>(key);
            if (setting is null)
                continue;

            if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                continue;

            if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                continue;

            var value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);

            //set property
            prop.SetValue(settings, value, null);
        }

#pragma warning disable CS8603 // Possible null reference return.
        return settings! as ISettings;
#pragma warning restore CS8603 // Possible null reference return.
    }

    /// <summary>
    /// Saves the specified settings asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to save, must implement <see cref="ISettings"/> and have a parameterless constructor.</typeparam>
    /// <param name="settings">The settings to save.</param>
    /// <remarks>
    /// This method iterates through the properties of the specified settings object, converts their values to strings, and saves them as key-value pairs.
    /// </remarks>
    public virtual async Task SaveSettingAsync<T>(T settings) where T : ISettings, new()
    {
        foreach (var prop in typeof(T).GetProperties())
        {
            // get properties we can read and write to
            if (!prop.CanRead || !prop.CanWrite)
                continue;

            if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                continue;

            var key = typeof(T).Name + "." + prop.Name;
            var value = prop.GetValue(settings, null);
            if (value != null)
                await SetSettingAsync(prop.PropertyType, key, value);
            else
                await SetSettingAsync(key, string.Empty);
        }
    }

    /// <summary>
    /// Saves the specified setting asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to save, must implement <see cref="ISettings"/> and have a parameterless constructor.</typeparam>
    /// <typeparam name="TPropType">The type of the property to save.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property to save.</param>
    /// <exception cref="ArgumentException">Thrown when the expression does not refer to a property.</exception>
    public virtual async Task SaveSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
    {
        if (keySelector.Body is not MemberExpression member)
            throw new ArgumentException($"Expression '{keySelector}' refers to a method, not a property.");

        var propInfo = member.Member as PropertyInfo;
        if (propInfo is null)
            throw new ArgumentException($"Expression '{keySelector}' refers to a field, not a property.");

        var key = GetSettingKey(settings, keySelector);
        var value = (TPropType)propInfo.GetValue(settings, null);

        if (value is not null)
            await SetSettingAsync(key, value);
        else
            await SetSettingAsync(key, string.Empty);
    }

    /// <summary>
    /// Sets the specified setting asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the setting value.</typeparam>
    /// <param name="key">The key of the setting.</param>
    /// <param name="value">The value of the setting.</param>
    public virtual async Task SetSettingAsync<T>(string key, T value)
    {
        await SetSettingAsync(typeof(T), key, value);
    }

    /// <summary>
    /// Inserts a new setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to insert.</param>
    public virtual async Task InsertSettingAsync(Setting setting)
    {
        await _settingRepository.InsertAsync(setting);
    }

    /// <summary>
    /// Updates an existing setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to update.</param>
    /// <exception cref="ArgumentNullException">Thrown when the setting is null.</exception>
    public virtual async Task UpdateSettingAsync(Setting setting)
    {
        if (setting == null)
            throw new ArgumentNullException(nameof(setting));

        await _settingRepository.UpdateAsync(setting);
    }

    /// <summary>
    /// Deletes a setting asynchronously.
    /// </summary>
    /// <param name="setting">The setting to delete.</param>
    public virtual async Task DeleteSettingAsync(Setting setting)
    {
        await _settingRepository.DeleteAsync(setting);
    }

    /// <summary>
    /// Deletes multiple settings asynchronously.
    /// </summary>
    /// <param name="settings">The list of settings to delete.</param>
    public virtual async Task DeleteSettingsAsync(IList<Setting> settings)
    {
        await _settingRepository.DeleteAsync(settings);
    }

    /// <summary>
    /// Deletes all settings of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to delete, must implement <see cref="ISettings"/> and have a parameterless constructor.</typeparam>
    /// <remarks>
    /// This method deletes all settings whose keys correspond to properties of the specified type.
    /// </remarks>
    public virtual async Task DeleteSettingAsync<T>() where T : ISettings, new()
    {
        var settingsToDelete = new List<Setting>();
        var allSettings = await GetAllSettingsAsync();

        foreach (var prop in typeof(T).GetProperties())
        {
            var key = typeof(T).Name + "." + prop.Name;
            settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
        }

        await DeleteSettingsAsync(settingsToDelete);
    }

    /// <summary>
    /// Deletes the specified setting asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to which the setting belongs, must implement <see cref="ISettings"/> and have a parameterless constructor.</typeparam>
    /// <typeparam name="TPropType">The type of the property representing the setting.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property of the setting to delete.</param>
    public virtual async Task DeleteSettingAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
    {
        var key = GetSettingKey(settings, keySelector);
        key = key.Trim().ToLowerInvariant();

        var allSettings = await GetAllSettingsDictionaryAsync();
        var setting = allSettings.TryGetValue(key, out var settings_) ? settings_.FirstOrDefault() : null;

        if (setting is null)
            return;

        //update
        var _setting = await GetSettingByIdAsync(setting.Id);
        await DeleteSettingAsync(_setting);
    }

    /// <summary>
    /// Checks if the specified setting exists asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of settings to which the setting belongs, must implement <see cref="ISettings"/> and have a parameterless constructor.</typeparam>
    /// <typeparam name="TPropType">The type of the property representing the setting.</typeparam>
    /// <param name="settings">The settings instance.</param>
    /// <param name="keySelector">Expression specifying the property of the setting to check.</param>
    /// <returns>True if the setting exists; otherwise, false.</returns>
    public virtual async Task<bool> SettingExistsAsync<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
    {
        var key = GetSettingKey(settings, keySelector);

        var setting = await GetSettingByKeyAsync<string>(key);
        return setting != null;
    }

    /// <summary>
    /// Sets the value of the specified setting asynchronously.
    /// </summary>
    /// <param name="type">The type of the setting value.</param>
    /// <param name="key">The key of the setting.</param>
    /// <param name="value">The value to set.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="key"/> is null.</exception>
    protected virtual async Task SetSettingAsync(Type type, string key, object value)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));
        key = key.Trim().ToLowerInvariant();
        var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

        var allSettings = await GetAllSettingsDictionaryAsync();

        var setting = allSettings.TryGetValue(key, out var settings) ? settings.FirstOrDefault() : null;

        if (setting is not null)
        {
            //update
            var _setting = await GetSettingByIdAsync(setting.Id);
            setting.Value = valueStr;
            await UpdateSettingAsync(_setting);
        }
        else
        {
            //insert
            var _setting = new Setting
            {
                Name = key,
                Value = valueStr
            };
            await InsertSettingAsync(_setting);
        }
    }

    /// <summary>
    /// Retrieves all settings as a dictionary asynchronously, where the keys are setting names and the values are lists of settings with the same name.
    /// </summary>
    /// <returns>A dictionary containing settings grouped by their names.</returns>
    protected virtual async Task<IDictionary<string, IList<Setting>>> GetAllSettingsDictionaryAsync()
    {
        var settings = await GetAllSettingsAsync();

        var dictionary = new Dictionary<string, IList<Setting>>();
        foreach (var s in settings)
        {
            var resourceName = s.Name.ToLowerInvariant();

            var settingForCaching = new Setting
            {
                Id = s.Id,
                Name = s.Name,
                Value = s.Value
            };

            if (!dictionary.ContainsKey(resourceName))
                //first setting
                dictionary.Add(resourceName, new List<Setting> { settingForCaching });
            else
                //already added
                dictionary[resourceName].Add(settingForCaching);
        }

        return dictionary;
    }
}