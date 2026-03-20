using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ModularMonolith.BuildingBlocks.EFCore;

public static class MigrateDbContextExtentions
{
    public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app)
       where TContext : DbContext
    {
        MigrateAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();

        SeedAsync(app.ApplicationServices).GetAwaiter().GetResult();

        return app;
    }

    private static async Task MigrateAsync<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            logger.LogInformation("Applying {Count} pending migrations...", pendingMigrations.Count());

            await context.Database.MigrateAsync();
            logger.LogInformation("Migrations applied successfully.");
        }
    }

    private static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var seedersManager = scope.ServiceProvider.GetRequiredService<ISeedManager>();

        await seedersManager.ExecuteSeedAsync();
    }

    public interface ISeedManager
    {
        Task ExecuteSeedAsync();
    }

    public class SeedManager(
    ILogger<SeedManager> logger,
    IWebHostEnvironment env,
    IServiceProvider serviceProvider
    ) : ISeedManager

    {
        public async Task ExecuteSeedAsync()
        {
            await using var scope = serviceProvider.CreateAsyncScope();
            var dataSeeders = scope.ServiceProvider.GetServices<IDataSeeder>();

            foreach (var seeder in dataSeeders)
            {
                logger.LogInformation("Seed {SeederName} is started.", seeder.GetType().Name);
                await seeder.SeedAllAsync();
                logger.LogInformation("Seed {SeederName} is completed.", seeder.GetType().Name);
            }
        }
    }

    public interface IDataSeeder
    {
        Task SeedAllAsync();
    }
}
