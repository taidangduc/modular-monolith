using Identity.Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IOpenIddictScopeManager _scopeManager;

    public AuthorizationController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IOpenIddictScopeManager scopeManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _scopeManager = scopeManager;
    }

    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        ClaimsPrincipal claimsPrincipal;

        if (request.IsRefreshTokenGrantType())
        {
            claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
        }
        else if (request.IsPasswordGrantType())
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
                });

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
                });

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var claimsIdentity = new ClaimsIdentity(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                OpenIddictConstants.Claims.Name,
                OpenIddictConstants.Claims.Role);

            claimsIdentity
                .SetClaim(OpenIddictConstants.Claims.Subject, await _userManager.GetUserIdAsync(user))
                .SetClaim(OpenIddictConstants.Claims.Email, await _userManager.GetEmailAsync(user))
                .SetClaim(OpenIddictConstants.Claims.Name, await _userManager.GetUserNameAsync(user));

            claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            claimsPrincipal.SetScopes(request.GetScopes());

            claimsPrincipal.SetDestinations(GetDestinations);

            claimsPrincipal.SetResources(await _scopeManager.ListResourcesAsync(claimsPrincipal.GetScopes()).ToListAsync());

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        else
        {
            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
    public static IEnumerable<string> GetDestinations(Claim claim)
    {
        return claim.Type switch
        {
            Claims.Name or
            Claims.Subject
                when claim.Subject!.HasScope(Scopes.Profile)
                   => new[] {
                       Destinations.AccessToken,
                       Destinations.IdentityToken
                   },

            _ => new[] { Destinations.AccessToken },
        };
    }
}
