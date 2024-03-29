using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Boba.Settings.EntityFrameworkCore.SqlServer;

/// <summary>
/// Provides methods to install SQL Server specific database objects.
/// </summary>
internal static class SqlServerObjectsInstaller
{
    /// <summary>
    /// Installs SQL Server objects using the provided connection string and default schema.
    /// </summary>
    /// <param name="connectionString">The connection string to the SQL Server database.</param>
    /// <exception cref="ArgumentNullException">Thrown when the connection string is null.</exception>
    public static void Install(string connectionString)
    {
        Install(connectionString, null);
    }

    /// <summary>
    /// Installs SQL Server objects using the provided connection string and schema name.
    /// </summary>
    /// <param name="connectionString">The connection string to the SQL Server database.</param>
    /// <param name="schema">The schema name for the SQL Server objects.</param>
    /// <exception cref="ArgumentNullException">Thrown when the connection string is null.</exception>
    public static void Install(string connectionString, string schema)
    {
        if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));

        using var connection = new SqlConnection(connectionString);
        var script = GetInstallScript(schema);
        using var command = connection.CreateCommand();
        command.CommandText = script;
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    /// <summary>
    /// Retrieves the install script for SQL Server objects based on the specified schema.
    /// </summary>
    /// <param name="schema">The schema name for the SQL Server objects.</param>
    /// <returns>The install script as a string.</returns>
    public static string GetInstallScript(string schema)
    {
        var assembly = typeof(SqlServerObjectsInstaller).GetTypeInfo().Assembly;
        var filePath = string.Format("{0}.Install.sql", assembly.GetName().Name);
        var script = GetStringResource(assembly, filePath);

        script = script.Replace("$(BobaSchema)", !string.IsNullOrWhiteSpace(schema) ? schema : Constants.SchemaName);

        return script;
    }

    /// <summary>
    /// Retrieves the content of a string resource embedded in the assembly.
    /// </summary>
    /// <param name="assembly">The assembly containing the embedded resource.</param>
    /// <param name="resourceName">The name of the embedded resource.</param>
    /// <returns>The content of the embedded resource as a string.</returns>
    private static string GetStringResource(Assembly assembly, string resourceName)
    {
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new InvalidOperationException(
                    $"Requested resource `{resourceName}` was not found in the assembly `{assembly}`.");
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}