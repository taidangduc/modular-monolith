using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.DbConfigurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.ToTable(nameof(UserClaim));
        builder.Property(x => x.Version).IsConcurrencyToken();
    }
}
