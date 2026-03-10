using BuildingBlocks.Constants;
using BuildingBlocks.Contracts;
using BuildingBlocks.Core;
using BuildingBlocks.Core.CQRS;
using Microsoft.AspNetCore.Identity;
using ModularMonolith.Identity.Domain.Entities;
using ModularMonolith.Identity.Domain.Exceptions;

namespace ModularMonolith.Identity.Features.Create;

public record CreateUserCommand(string FirstName, string LastName, string UserName,
    string Email, string Password, string ConfirmPassword) : ICommand<Guid>;

internal class CreateUserHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly UserManager<User> _userManager;

    public CreateUserHandler(
        IEventDispatcher eventDispatcher,
        UserManager<User> userManager)
    {
        _eventDispatcher = eventDispatcher;
        _userManager = userManager;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.Password
        };

        var identityResult = await _userManager.CreateAsync(user, request.Password);
        var roleResult = await _userManager.AddToRoleAsync(user, IdentityConstant.Role.User);

        if (!identityResult.Succeeded)
        {
            throw new InvalidUserException(string.Join(',', identityResult.Errors.Select(x => x.Description)));
        }

        if (!roleResult.Succeeded)
        {
            throw new InvalidUserRoleException(string.Join(',', roleResult.Errors.Select(x => x.Description)));
        }

        await _eventDispatcher.SendAsync(
            new UserCreated(user.Id, user.UserName, user.FirstName + " " + user.LastName,
                user.Email), cancellationToken: cancellationToken);

        return user.Id;
    }
}
