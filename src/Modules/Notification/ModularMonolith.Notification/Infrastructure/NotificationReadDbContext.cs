using BuildingBlocks.EFCore;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularMonolith.Notification.Infrastructure.Projections;

namespace ModularMonolith.Notification.Infrastructure;

public class NotificationReadDbContext : DbContextBase, IDbContext
{
    public NotificationReadDbContext(
        DbContextOptions options, 
        ILogger<DbContextBase>? logger = null,
        ICurrentUserProvider? currentUserProvider = null) : base(options, logger, currentUserProvider)
    {
    }

    public DbSet<PreferenceView> preferenceView => Set<PreferenceView>();
    public DbSet<ProfileView> profileView => Set<ProfileView>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(NotificationRoot).Assembly);

        builder.Entity<ProfileView>().ToTable(nameof(ProfileView));
        builder.Entity<PreferenceView>().ToTable(nameof(PreferenceView));
        builder.Entity<PreferenceView>().HasIndex(x => new { x.UserId, x.Channel });    
    }
}
