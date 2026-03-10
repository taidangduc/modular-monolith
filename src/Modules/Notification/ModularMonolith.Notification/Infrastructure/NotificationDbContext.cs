using BuildingBlocks.EFCore;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularMonolith.Notification.Domain.Entities;

namespace ModularMonolith.Notification.Infrastructure;
public class NotificationDbContext : AppDbContextBase
{
    public NotificationDbContext(
        DbContextOptions<NotificationDbContext> options,
        ILogger<AppDbContextBase>? logger = null,
        ICurrentUserProvider? currentUserProvider = null) : base(options, logger, currentUserProvider)
    {
    }

    public DbSet<EmailMessage> EmailMessages => Set<EmailMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(NotificationRoot).Assembly);

    }
}
