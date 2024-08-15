using Boba.Settings.Attributes;

namespace Boba.Settings.EntityFrameworkCore.InMemory.WebApp;

[BobaSettingsDisplayOrder(1)]
public class TestSettings : IBobaSettings
{
    [BobaPropertyDisplayOrder(2)]
    public bool Enabled { get; set; } = true;

    [BobaPropertyDisplayOrder(1)]
    public int DefaultLangId { get; set; } = default!;

    [BobaPropertyDisplayOrder(3)]
    public string DefaultColor { get; set; } = "Red";
}

[BobaSettingsDisplayOrder(2)]
public class ERADPaymentSettings : IBobaSettings
{
    [BobaPropertyDisplayOrder(1)]
    public int PaymentId { get; set; }

    [BobaPropertyDisplayOrder(2)]
    public string ProviderName { get; set; } = default!;

    [BobaPropertyDisplayOrder(3)]
    public bool IsEnabled { get; set; }

    [BobaPropertyDisplayOrder(4)]
    public string Currency { get; set; } = default!;

    [BobaPropertyDisplayOrder(5)]
    public int Timeout { get; set; }

    [BobaPropertyDisplayOrder(6)]
    public string ApiKey { get; set; } = default!;
}
[BobaSettingsDisplayOrder(4)]
public class PaypalSettings : IBobaSettings
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public bool IsSandbox { get; set; }
    public string CurrencyCode { get; set; } = default!;
    public string RedirectUrl { get; set; } = default!;
}
[BobaSettingsDisplayOrder(3)]
public class DubaiGovtSettings : IBobaSettings
{
    public int GovId { get; set; }
    public string ServiceName { get; set; } = default!;
    public bool IsActive { get; set; }
    public string EndPointUrl { get; set; } = default!;
    public int MaxRetryAttempts { get; set; }
}
