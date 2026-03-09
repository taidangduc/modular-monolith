using BuildingBlocks.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities;

public class Role : IdentityRole<Guid>, IVersion
{
    public long Version { get; set; }
}
