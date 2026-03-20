using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModularMonolith.BuildingBlocks.EFCore;

namespace BuildingBlocks.EFCore;

public static class DbContextExtensions
{
    public static void AddCustomDbContext<TDbContext>(
        this IHostApplicationBuilder builder,
        string name,
        Action<IHostApplicationBuilder>? action = null,
        bool haveInterceptors = false)
        where TDbContext : DbContext
    {
        if (haveInterceptors)
        {
            builder.Services.AddScoped<IInterceptor, DispatchDomainEventInterceptor>();
        }

        builder.Services.AddDbContext<TDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString(name));

            var interceptors = sp.GetServices<IInterceptor>().ToArray();

            if(interceptors.Length != 0)
            {
                options.AddInterceptors(interceptors);
            }
        });

        action?.Invoke(builder);
    }


    public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app)
        where TContext : DbContext, IDbContext
    {
        MigrateAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();

        SeedAsync(app.ApplicationServices).GetAwaiter().GetResult();

        return app;
    }

    private static async Task MigrateAsync<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext, IDbContext
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

}
