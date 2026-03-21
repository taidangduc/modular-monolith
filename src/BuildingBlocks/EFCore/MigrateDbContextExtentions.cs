using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.BuildingBlocks.EFCore;

public static class MigrateDbContextExtentions
{
    public static async Task<IHost> MigrateDbContextAsync<TContext>(
        this IHost host,
        CancellationToken cancellationToken = default)
            where TContext : DbContext
    {
        await using var scope = host.Services.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        await MigrateAsync(context, logger, cancellationToken);

        return host;
    }
    public static async Task<IHost> MigrateAndSeedDbContextAsync<TContext>(
        this IHost host,
        CancellationToken cancellationToken = default)
            where TContext : DbContext
    {
        await using var scope = host.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetService<TContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        if (context is not null)
        {
            try
            {
                await MigrateAsync(context, logger, cancellationToken);

                var seeders = scope.ServiceProvider.GetServices<IDataSeeder<TContext>>();

                if (seeders is null || !seeders.Any())
                {
                    logger.LogInformation("No seeders found for DbContext {DbContext}", typeof(TContext).Name);
                    return host;
                }

                foreach (var seeder in seeders)
                {
                    logger.LogInformation("Seeding {Seeder} for DbContext {DbContext}", seeder.GetType().Name, typeof(TContext).Name);

                    await seeder.SeedAsync(cancellationToken);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                throw;
            }
        }

        return host;
    }

    public static async Task MigrateAsync<TContext>(TContext context, ILogger<TContext> logger, CancellationToken cancellationToken = default)
       where TContext : DbContext
    {
        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);

            if (pendingMigrations.Any())
            {
                logger.LogInformation("Applying {Count} pending migrations...", pendingMigrations.Count());

                await context.Database.MigrateAsync(cancellationToken);

                logger.LogInformation("Database migrations applied successfully.");
            }
        });
    }

    public interface IDataSeeder<in TContext> where TContext : DbContext
    {
        Task SeedAsync(CancellationToken cancellationToken);
    }
}
