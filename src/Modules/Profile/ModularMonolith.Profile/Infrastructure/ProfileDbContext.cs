using ModularMonolith.BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ModularMonolith.Profile.Infrastructure;

public class ProfileDbContext : DbContextUnitOfWork<ProfileDbContext>
{
    public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Profile> Profiles => Set<Domain.Entities.Profile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
