namespace Boba.Settings.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BobaSettingsDisplayOrderAttribute : Attribute
{
    public int Order { get; }

    public BobaSettingsDisplayOrderAttribute(int order)
    {
        Order = order;
    }
}

[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public class BobaPropertyDisplayOrderAttribute : Attribute
{
    public int Order { get; }

    public BobaPropertyDisplayOrderAttribute(int order)
    {
        Order = order;
    }
}
