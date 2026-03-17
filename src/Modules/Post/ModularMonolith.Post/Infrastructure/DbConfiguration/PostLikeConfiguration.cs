using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ModularMonolith.Post.Infrastructure.DbConfiguration;

public class PostLikeConfiguration : IEntityTypeConfiguration<Domain.Entities.PostLike>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PostLike> builder)
    {
        builder.ToTable("PostLikes");

        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Version).IsConcurrencyToken();

        builder.Property(x => x.PostId).IsRequired();

        builder.Property(x => x.UserId).IsRequired();

        builder.HasIndex(x => new { x.PostId, x.UserId }).IsUnique();
    }
}
