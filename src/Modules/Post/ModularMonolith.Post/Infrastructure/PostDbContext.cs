using BuildingBlocks.EFCore;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.Post.Infrastructure;

public class PostDbContext : DbContextBase, IDbContext
{
    public PostDbContext(
        DbContextOptions<PostDbContext> options,
        ILogger<DbContextBase>? logger = null,
        ICurrentUserProvider? currentUserProvider = null)
        : base(options, logger, currentUserProvider)
    {
    }

    public DbSet<Domain.Entities.Post> Posts => Set<Domain.Entities.Post>();
    public DbSet<Domain.Entities.PostLike> PostLikes => Set<Domain.Entities.PostLike>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostDbContext).Assembly);
    }
}
