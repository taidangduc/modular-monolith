using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.DbConfigurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(nameof(UserToken));
        builder.Property(x => x.Version).IsConcurrencyToken();
    }
}
