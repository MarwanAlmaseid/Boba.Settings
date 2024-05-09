
# Boba.Settings (EF)

Boba.Settings is a comprehensive C# library meticulously crafted to streamline the management of application and business settings within projects. Offering a suite of robust functionalities, it empowers developers to efficiently organize and access crucial configurations, thereby enhancing development workflows and ensuring seamless integration of settings across various facets of the application.

## Getting Started

### Installation

Boba.Settings is conveniently available on NuGet. Simply install the provider package corresponding to your database.

Choose your data store option and follow its documentation to install the right package.
- Sql Server: [Boba.Settings.EntityFrameworkCore.SqlServer](https://github.com/MarwanAlmaseid/Boba.Settings/tree/master/src/Boba.Settings.EntityFrameworkCore.SqlServer)
- In Memeory: [Boba.Settings.EntityFrameworkCore.InMemory](https://github.com/MarwanAlmaseid/Boba.Settings/tree/master/src/Boba.Settings.EntityFrameworkCore.InMemory)


Utilize the `--version` option to specify a preview version for installation if needed.

### Basic Usage
Harnessing the power of Boba.Settings is straightforward. Follow these steps to get started:

#### Registration: 
Begin by registering Boba.Settings in your `program.cs`. Presently, we support Sql server, InMemeory, Alternative data store providers are also available.
```c#
builder.Services.AddBobaSettings();
```
#### Configuration:
Define your settings class, inheriting from `ISettings`. Here, you can optionally assign default values to be retrieved in the absence of stored data.

```c#
public class TestSettings : ISettings
{
    public bool Enabled { get; set; } = true;
    public int DefaultLangId { get; set; } = default!;
    public string DefaultColor { get; set; } = "Red";
}
```

#### Utilization: 
Inject the setting service into your application and access the values effortlessly.
```c#
private readonly TestSettings _testSettings;
	
public YourConstructor(TestSettings testSettings)
{
    _testSettings = testSettings;
}

Console.WriteLine(_testSettings.DefaultLangId);

```
Additionally, settings can be loaded manually.

```c#
private readonly ISettingService _settingService;
	
public YourConstructor(ISettingService settingService)
{
    _settingService = settingService;
}

var testSettings = await _settingService.LoadSettingAsync<TestSettings>();

Console.WriteLine(testSettings.DefaultLangId);
```

#### Management:
The ISettingService interface offers a plethora of APIs for seamless management of settings. For instance, to insert settings:
```c#
private readonly ISettingService _settingService;
	
public YourConstructor(ISettingService settingService)
{
    _settingService = settingService;
}

await _settingService.SaveSettingAsync(new TestSettings { DefaultLangId = 1, Enabled = false });
```

### Available Data Stores
- Sql Server: [Boba.Settings.EntityFrameworkCore.SqlServer](https://github.com/MarwanAlmaseid/Boba.Settings/tree/master/src/Boba.Settings.EntityFrameworkCore.SqlServer)
- In Memeory: [Boba.Settings.EntityFrameworkCore.InMemory](https://github.com/MarwanAlmaseid/Boba.Settings/tree/master/src/Boba.Settings.EntityFrameworkCore.InMemory)
### Upcoming Data Stores
- Sql light
- Azure Cosmos DB SQL API
- PostgreSQL
- MySQL, MariaDB
- Oracle DB 11.2 onwards
- And more…
Contributions to expand the list of supported data stores are highly encouraged.
### Contributing

Contributions are always welcome!

See `contributing.md` for ways to get started.

Please adhere to this project's `code of conduct`.


### Upcoming Features
- Comprehensive unit testing capabilities.
- Query caching mechanisms for enhanced performance.
- Intuitive User Interface for simplified management.
- Flexible injection options, including Windsor, Unity, Autofac, etc.
- Support for various DB stores such as MangoDb, Sql Light, CozmoDB.
- Association of settings with entities like UserId, StoreId, etc.
- Setting version
### Versions
The main branch is now on .NET 8.0. Previous versions are not available at this time.

### License
This project is licensed under the [MIT](https://choosealicense.com/licenses/mit/) license.

### Support
If you encounter any issues or have questions, please feel free to [raise a new issue](https://github.com/MarwanAlmaseid/Boba.Settings/issues).

### Technologies

 - [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)
 - [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/)
