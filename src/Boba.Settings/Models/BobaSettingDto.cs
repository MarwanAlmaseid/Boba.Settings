namespace Boba.Settings.Models;

/// <summary>
/// Represents a DTO for settings.
/// </summary>
public class BobaSettingDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the setting.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the group name of the setting.
    /// </summary>
    public string GroupName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the setting.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the full name of the setting.
    /// </summary>
    public string FullName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the setting.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the default value of the setting.
    /// </summary>
    public string DefaultValue { get; set; } = default!;

    /// <summary>
    /// Gets or sets the current value of the setting.
    /// </summary>
    public string Value { get; set; } = default!;
}
