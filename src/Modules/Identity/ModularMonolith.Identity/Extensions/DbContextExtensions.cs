
using BuildingBlocks.Configuration;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Identity.Infrastructure;

namespace ModularMonolith.Identity.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddCustomIdentityContext(this IServiceCollection services)
    {
        var mssqlOptions = services.GetOptions<MssqlOptions>("mssql").ConnectionString;

        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(mssqlOptions);

            //use schema
            options.UseOpenIddict();
        });

        //services.AddScoped<ISeedManager, SeedManager>();

        return services;
    }
}
