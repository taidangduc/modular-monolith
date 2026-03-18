using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularMonolith.BuildingBlocks.Contracts;

namespace ModularMonolith.Preference.Infrastructure.DbConfiguration;

public class PreferenceConfiguration : IEntityTypeConfiguration<Domain.Entities.Preference>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Preference> builder)
    {
        builder.ToTable("Preferences");

        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Channel)
            .HasDefaultValue(ChannelType.Web)
            .HasConversion(
                x => x.ToString(),
                x => (ChannelType)Enum.Parse(typeof(ChannelType), x));
    }
}
