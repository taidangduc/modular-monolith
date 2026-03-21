namespace ModularMonolith.BuildingBlocks.Core;

public static class Authentization
{
    public static class Roles
    {
        public const string Admin = "admin";
        public const string User = "user";
    }

    public static class Policies
    {
        public const string Admin = nameof(Roles.Admin);
        public const string User = nameof(Roles.User);
    }

    public static class Permissions
    {
        public const string Read = "read";
        public const string Write = "write";
    }
}