
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.EFCore;

namespace ModularMonolith.Post.Infrastructure;

public class PostDbContext : DbContextUnitOfWork<PostDbContext>
{
    public PostDbContext(DbContextOptions<PostDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Entities.Post> Posts => Set<Domain.Entities.Post>();
    public DbSet<Domain.Entities.PostLike> PostLikes => Set<Domain.Entities.PostLike>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
