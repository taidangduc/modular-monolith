namespace ModularMonolith.Api.ConfigurationOptions;

public class AppSettings
{
    public ModuleOptions ModuleOptions { get; set; }
    public AuthenticationOptions AuthenticationOptions { get; set; }
    public CORS CORS { get; set; }
}
