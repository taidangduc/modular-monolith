using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Domain.Enums;

namespace User.Infrastructure.DbConfigurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Version).IsConcurrencyToken();

        builder.Property(x => x.GenderType)
            .HasDefaultValue(GenderType.Unknown)
            .HasConversion(
                x => x.ToString(),
                x => (GenderType)Enum.Parse(typeof(GenderType), x));
    }
}
