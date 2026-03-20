using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ModularMonolith.BuildingBlocks.EFCore;

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

            if (interceptors.Length != 0)
            {
                options.AddInterceptors(interceptors);
            }
        });

        action?.Invoke(builder);
    }
}
