namespace Boba.Settings.EntityFrameworkCore.SqlServer.WebApp;

public class TestSettings : IBobaSettings
{
    public bool Enabled { get; set; } = true;
    public int DefaultLangId { get; set; } = default!;
    public string DefaultColor { get; set; } = "Red";
}
