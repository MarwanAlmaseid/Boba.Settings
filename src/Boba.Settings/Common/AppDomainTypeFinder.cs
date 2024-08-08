using System.Reflection;

namespace Boba.Settings;

/// <summary>
/// Helper class for finding types in the current AppDomain.
/// </summary>
public class AppDomainTypeFinder
{
    /// <summary>
    /// Finds classes assignable to the specified type in the current AppDomain.
    /// </summary>
    /// <param name="assignTypeFrom">The type to which the found classes should be assignable.</param>
    /// <param name="onlyConcreteClasses">Specifies whether to include only concrete classes (default: true).</param>
    /// <returns>An enumerable collection of found types.</returns>
    public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var allTypes = new List<Type>();

        foreach (var assembly in assemblies)
        {
            Type[] types;

            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types!;
            }

            if (types != null)
            {
                allTypes.AddRange(
                    types.Where(x =>
                        x != null
                        && assignTypeFrom.IsAssignableFrom(x)
                        && (onlyConcreteClasses ? x.IsClass && !x.IsAbstract : true)
                    )
                );
            }
        }

        return allTypes;
    }
}
