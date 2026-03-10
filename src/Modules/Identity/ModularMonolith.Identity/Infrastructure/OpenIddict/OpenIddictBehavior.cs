using OpenIddict.Abstractions;
using OpenIddict.Server;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace ModularMonolith.Identity.Infrastructure.OpenIddict;

public class OpenIddictBehavior : IOpenIddictServerHandler<OpenIddictServerEvents.ValidateTokenRequestContext>
{
    public async ValueTask HandleAsync(OpenIddictServerEvents.ValidateTokenRequestContext context)
    {
        if (context.Request.IsPasswordGrantType())
        {
            if (string.IsNullOrEmpty(context.Request.Username) ||
                string.IsNullOrEmpty(context.Request.Password))
            {
                context.Reject(error: Errors.InvalidRequest, description: "Missing credentials.");
            }
        }

        return;
    }
}
