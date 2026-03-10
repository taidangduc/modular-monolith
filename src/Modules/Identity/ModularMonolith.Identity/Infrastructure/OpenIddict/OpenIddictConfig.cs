using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace ModularMonolith.Identity.Infrastructure.OpenIddict;

public static class OpenIddictConfig
{
    public static string[] ApiScopes =>
        new string[]
        {
            new(Scopes.OpenId),
            new(Scopes.Email),
            new(Scopes.Profile),
            new(Constants.StandardScope.PostApi),
            new(Constants.StandardScope.NotificationApi),
            new(Constants.StandardScope.ProfileApi),
            new(Constants.StandardScope.IdentityApi),
            new(Constants.StandardScope.ModularMonolith),
            new(Scopes.Roles),
        };

    public static OpenIddictApplicationDescriptor OpenIddictClient =>
        new OpenIddictApplicationDescriptor
        {
            ClientId = "client",
            ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
            ClientType = ClientTypes.Confidential,
            RedirectUris =
            {
               new Uri("https://localhost:7265/connect/token")
            },
            Permissions =
            {
                Permissions.Endpoints.Token,
                Permissions.Endpoints.Authorization,

                Permissions.GrantTypes.ClientCredentials,
                Permissions.GrantTypes.Password,
                Permissions.GrantTypes.RefreshToken,

                Permissions.ResponseTypes.Code,

                Permissions.ResponseTypes.IdToken,

                Permissions.Scopes.Roles,
                Permissions.Scopes.Email,
                Permissions.Scopes.Profile,

                Permissions.Prefixes.Scope + "openid",
                Permissions.Prefixes.Scope + Constants.StandardScope.PostApi,
                Permissions.Prefixes.Scope + Constants.StandardScope.ProfileApi,
                Permissions.Prefixes.Scope + Constants.StandardScope.NotificationApi,
                Permissions.Prefixes.Scope + Constants.StandardScope.ModularMonolith,
            },
        };
}
