using BuildingBlocks.EFCore;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.Preference.Infrastructure;

public class PreferenceDbContext : DbContextBase
{
    public PreferenceDbContext(
        DbContextOptions<PreferenceDbContext> options,
        ILogger<DbContextBase>? logger = null,
        ICurrentUserProvider? currentUserProvider = null)
        : base(options, logger, currentUserProvider)
    {
    }

    public DbSet<Domain.Entities.Preference> Preferences => Set<Domain.Entities.Preference>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PreferenceDbContext).Assembly);
    }
}