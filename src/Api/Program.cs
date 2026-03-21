using ModularMonolith.Api.ConfigurationOptions;
using ModularMonolith.Api.Extensions;
using ModularMonolith.Identity.Extensions;
using ModularMonolith.Notification.Extensions;
using ModularMonolith.Preference.Extensions;
using ModularMonolith.Profile.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

services.Configure<AppSettings>(configuration);

builder
.AddIdentityModule(opt => configuration.GetSection("Modules:Identity").Bind(opt))
.AddNotificationModule((opt) => configuration.GetSection("Modules:Notification").Bind(opt))
.AddProfileModule((opt) => configuration.GetSection("Modules:Profile").Bind(opt))
.AddPreferenceModule((opt) => configuration.GetSection("Modules:Preference").Bind(opt))
.AddApplicationServices(appSettings);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app
.UseNotificationModule()
.UseProfileModule()
.UsePreferenceModule()
.UseApplicationServices();

await app.UseIdentityModuleAsync();

app.Run();
