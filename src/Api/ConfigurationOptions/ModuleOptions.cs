using ModularMonolith.Identity.ConfigurationOptions;
using ModularMonolith.Notification.ConfigurationOptions;
using ModularMonolith.Preference.ConfigurationOptions;
using ModularMonolith.Profile.ConfigurationOptions;

namespace ModularMonolith.Api.ConfigurationOptions;

public class ModuleOptions
{
    public NotificationModuleOptions Notification { get; set; }
    public IdentityModuleOptions Identity { get; set; }
    public ProfileModuleOptions Profile { get; set; }
    public PreferenceModuleOptions Preference { get; set; }
}
