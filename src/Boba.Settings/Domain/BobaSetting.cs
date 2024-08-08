using Boba.Settings;

/// <summary>
/// Represents a setting
/// </summary>
public class BobaSetting : BobaBaseEntity
{
    /// <summary>
    /// Gets or sets the setting name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the setting value.
    /// </summary>
    public string Value { get; set; } = default!;
}