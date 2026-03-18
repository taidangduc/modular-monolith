using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularMonolith.Profile.Domain.Enums;

namespace ModularMonolith.Profile.Infrastructure.DbConfiguration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Domain.Entities.Profile>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Profile> builder)
        {
            builder.ToTable("Profiles");

            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

            builder.Property(x => x.GenderType)
                .HasDefaultValue(GenderType.Unknown)
                .HasConversion(
                    x => x.ToString(),
                    x => (GenderType)Enum.Parse(typeof(GenderType), x));
        }
    }

}
