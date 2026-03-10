using BuildingBlocks.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace ModularMonolith.Identity.Domain.Entities;

public class User : IdentityUser<Guid>, IVersion
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long Version { get; set; }
}
