using ModularMonolith.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.BuildingBlocks.EventBus;
using static ModularMonolith.BuildingBlocks.EFCore.MigrateDbContextExtentions;
using ModularMonolith.Identity.Domain.Events;
using ModularMonolith.BuildingBlocks.Core;

namespace ModularMonolith.Identity.Infrastructure.Seeds;

public class UserSeeder : IDataSeeder<IdentityDbContext>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IEventDispatcher _dispatcher;
    private readonly IdentityDbContext _identityContext;

    public UserSeeder(UserManager<User> userManager, RoleManager<Role> roleManager,
        IEventDispatcher dispatcher, IdentityDbContext identityContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dispatcher = dispatcher;
        _identityContext = identityContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var pendingMigrations = await _identityContext.Database.GetPendingMigrationsAsync();

        if (!pendingMigrations.Any())
        {
            await SeedRoles();
            await SeedUsers();
        }
    }

    private async Task SeedRoles()
    {
        if (!await _identityContext.Roles.AnyAsync())
        {
            if (await _roleManager.RoleExistsAsync(Authentization.Roles.Admin) == false)
            {
                await _roleManager.CreateAsync(new Role { Name = Authentization.Roles.Admin });
            }

            if (await _roleManager.RoleExistsAsync(Authentization.Roles.User) == false)
            {
                await _roleManager.CreateAsync(new Role { Name = Authentization.Roles.User });
            }
        }
    }

    private async Task SeedUsers()
    {
        if (!await _identityContext.Users.AnyAsync())
        {
            if (await _userManager.FindByNameAsync("peter") == null)
            {
                var result = await _userManager.CreateAsync(InitialData.Users.First(), "admin@123456");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(InitialData.Users.First(), Authentization.Roles.Admin);

                    await _dispatcher.DispatchAsync(
                        new UserCreatedEvent(
                            InitialData.Users.First().Id, 
                            InitialData.Users.First().FirstName + " " + InitialData.Users.First().LastName, 
                            InitialData.Users.First().Email!));
                }
            }

            if (await _userManager.FindByNameAsync("mira") == null)
            {
                var result = await _userManager.CreateAsync(InitialData.Users.Last(), "user@123456");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(InitialData.Users.Last(), Authentization.Roles.User);

                    await _dispatcher.DispatchAsync(
                        new UserCreatedEvent(
                            InitialData.Users.Last().Id, 
                            InitialData.Users.Last().FirstName + " " + InitialData.Users.Last().LastName, 
                            InitialData.Users.Last().Email!));
                }
            }
        }
    }
}
