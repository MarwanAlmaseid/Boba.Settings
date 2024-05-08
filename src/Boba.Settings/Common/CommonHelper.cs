using System.ComponentModel;
using System.Globalization;

namespace Boba.Settings;

/// <summary>
/// Helper class providing common conversion methods.
/// </summary>
public class CommonHelper
{
    /// <summary>
    /// Converts the specified value to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the value to.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    public static T To<T>(object value)
    {
        return (T)To(value, typeof(T));
    }

    /// <summary>
    /// Converts the specified value to the specified destination type.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">The destination type to convert the value to.</param>
    /// <returns>The converted value.</returns>
    public static object To(object value, Type destinationType)
    {
        return To(value, destinationType, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts the specified value to the specified destination type with the specified culture.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">The destination type to convert the value to.</param>
    /// <param name="culture">The culture information to use during conversion.</param>
    /// <returns>The converted value.</returns>
    public static object To(object value, Type destinationType, CultureInfo culture)
    {
        if (value == null)
            return null!;

        var sourceType = value.GetType();

        var destinationConverter = TypeDescriptor.GetConverter(destinationType);
        if (destinationConverter.CanConvertFrom(value.GetType()))
            return destinationConverter.ConvertFrom(null, culture, value)!;

        var sourceConverter = TypeDescriptor.GetConverter(sourceType);
        if (sourceConverter.CanConvertTo(destinationType))
            return sourceConverter.ConvertTo(null, culture, value, destinationType)!;

        if (destinationType.IsEnum && value is int)
            return Enum.ToObject(destinationType, (int)value);

        if (!destinationType.IsInstanceOfType(value))
            return Convert.ChangeType(value, destinationType, culture);

        return value;
    }
}
