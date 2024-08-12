using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boba.Settings.EntityFrameworkCore;

/// <summary>
/// Entity type configuration for setting.
/// </summary>
public class BobaSettingConfiguration : IEntityTypeConfiguration<BobaSetting>
{
    public void Configure(EntityTypeBuilder<BobaSetting> builder)
    {
        builder.ToTable("Setting", Constants.SchemaName);

        builder.HasKey(x => x.Id);
        builder.Property(e => e.Name).HasMaxLength(512).IsRequired();
        builder
            .Property(e => e.Value)
            .HasMaxLength(int.MaxValue)
            .HasDefaultValue(string.Empty)
            .IsRequired(false);
    }
}
