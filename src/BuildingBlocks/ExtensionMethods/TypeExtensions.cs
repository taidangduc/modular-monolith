namespace ModularMonolith.BuildingBlocks.ExtensionMethods;

public static class TypeExtensions
{
    public static Type? GetTypeFromCurrentDomainAssembly(string typeName)
    {
        var result = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a
                .GetTypes()
                .Where(x => x.FullName == typeName || x.Name == typeName))
            .FirstOrDefault();

        return result;
    }
}
