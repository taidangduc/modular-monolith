using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.DbConfigurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable(nameof(RoleClaim));
        builder.Property(x => x.Version).IsConcurrencyToken();
    }
}
