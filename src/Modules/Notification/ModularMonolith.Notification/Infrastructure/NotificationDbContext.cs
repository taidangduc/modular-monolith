
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.EFCore;
using ModularMonolith.Notification.Domain.Entities;

namespace ModularMonolith.Notification.Infrastructure;

public class NotificationDbContext : DbContextUnitOfWork<NotificationDbContext>
{
    public NotificationDbContext( DbContextOptions<NotificationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<EmailMessage> EmailMessages => Set<EmailMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
