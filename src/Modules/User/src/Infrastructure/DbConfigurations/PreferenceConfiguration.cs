using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using BuildingBlocks.Contracts;

namespace User.Infrastructure.DbConfigurations;

public class PreferenceConfiguration : IEntityTypeConfiguration<Preference>
{
    public void Configure(EntityTypeBuilder<Preference> builder)
    {
        builder.ToTable("Preferences");

        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Version).IsConcurrencyToken();

        builder.Property(x => x.Channel)
            .HasDefaultValue(ChannelType.Web)
            .HasConversion(
                x => x.ToString(),
                x => (ChannelType)Enum.Parse(typeof(ChannelType), x));
    }
}

