using BuildingBlocks.Core.Model;
using User.Domain.Enums;

namespace User.Domain.Entities;

public record Profile : Aggregate<Guid>
{
    public Guid UserId { get; private set; } = default!;
    public string UserName { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public GenderType GenderType { get; private set; }
    public int? Age { get; private set; }

    public static Profile Create( Guid userId, string userName, string name, string email, bool isDeleted = false)
    {
        var profile = new Profile
        {
            UserId = userId,
            UserName = userName,
            Name = name,
            Email = email,
            IsDeleted = isDeleted
        };

        return profile;
    }

    public void Update(Guid userId, string userName, string name, string email, GenderType genderType, int age, bool isDeleted = false)
    {
        this.UserId = userId;
        this.UserName = userName;
        this.Name = name;
        this.Email = email;
        this.GenderType = genderType;
        this.Age = age;
        this.IsDeleted = isDeleted;
    }
}

