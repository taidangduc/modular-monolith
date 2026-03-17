using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ModularMonolith.Post.Infrastructure.DbConfiguration;

public class PostConfiguration : IEntityTypeConfiguration<Domain.Entities.Post>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Post> builder)
    {
        builder.ToTable("Posts");

        builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Version).IsConcurrencyToken();

        builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);

        builder.Property(x => x.AuthorId).IsRequired();
    }
}
