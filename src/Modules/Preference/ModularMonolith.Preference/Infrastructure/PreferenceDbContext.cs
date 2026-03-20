
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.EFCore;

namespace ModularMonolith.Preference.Infrastructure;

public class PreferenceDbContext : DbContextUnitOfWork<PreferenceDbContext>
{
    public PreferenceDbContext(DbContextOptions<PreferenceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Entities.Preference> Preferences => Set<Domain.Entities.Preference>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}