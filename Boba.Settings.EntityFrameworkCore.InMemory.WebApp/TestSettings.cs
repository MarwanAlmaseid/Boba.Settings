namespace Boba.Settings.EntityFrameworkCore.InMemory.WebApp;

public class TestSettings : ISettings
{
    public bool Enabled { get; set; } = true;
    public int DefaultLangId { get; set; } = default!;
    public string DefaultColor { get; set; } = "Red";
}
