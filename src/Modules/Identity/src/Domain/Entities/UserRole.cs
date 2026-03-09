using BuildingBlocks.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities;

public class UserRole : IdentityUserRole<Guid>, IVersion
{
    public long Version { get; set; }
}
