using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.EFCore;
using ModularMonolith.Notification.Infrastructure.Projections;

namespace ModularMonolith.Notification.Infrastructure;

public class NotificationReadDbContext : DbContextUnitOfWork<NotificationReadDbContext>
{
    public NotificationReadDbContext(DbContextOptions<NotificationReadDbContext> options) 
        : base(options)
    {
    }

    public DbSet<PreferenceView> PreferenceView => Set<PreferenceView>();
    public DbSet<ProfileView> ProfileView => Set<ProfileView>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(NotificationRoot).Assembly);

        builder.Entity<ProfileView>().ToTable(nameof(ProfileView));
        builder.Entity<ProfileView>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");

        builder.Entity<PreferenceView>().ToTable(nameof(PreferenceView));
        builder.Entity<ProfileView>().Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        builder.Entity<PreferenceView>().HasIndex(x => new { x.UserId, x.Channel });    
    }
}
