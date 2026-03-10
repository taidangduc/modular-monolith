using ModularMonolith.Identity.Infrastructure.OpenIddict;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;

namespace ModularMonolith.Identity.Infrastructure.HostServices;
//ref: https://documentation.openiddict.com/configuration/application-permissions
public class SeedDataHostServices : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    public SeedDataHostServices(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        if (await manager.FindByClientIdAsync("client") is null)
        {        
            await manager.CreateAsync(OpenIddictConfig.OpenIddictClient);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
