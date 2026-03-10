using BuildingBlocks.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace ModularMonolith.Identity.Domain.Entities;

public class UserToken : IdentityUserToken<Guid>, IVersion
{
    public long Version { get; set; }
}
