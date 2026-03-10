using BuildingBlocks.EFCore;
using BuildingBlocks.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.Profile.Infrastructure;

public class ProfileDbContext : AppDbContextBase
{
    public ProfileDbContext(
        DbContextOptions<ProfileDbContext> options,
        ILogger<AppDbContextBase>? logger = null,
        ICurrentUserProvider? currentUserProvider = null)
        : base(options, logger, currentUserProvider)
    {
    }

    public DbSet<Domain.Entities.Profile> Profiles => Set<Domain.Entities.Profile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileDbContext).Assembly);
    }
}
