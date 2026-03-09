using Identity.Domain.Entities;

namespace Identity.Infrastructure.Seeds;

public static class InitialData
{
    public static List<User> Users { get; }
    static InitialData()
    {
        Users = new List<User> {
             new User()
             {
                Id = Guid.NewGuid(),
                FirstName = "Peter",
                LastName = "Mark",
                UserName = "peter",
                Email = "peter@test.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                Version = 1
             },
            new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mira",
                LastName = "Lee",
                UserName = "mira",
                Email = "mira@test.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                Version = 1
           }
        };      
    }
}
