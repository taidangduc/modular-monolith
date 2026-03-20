using Api.Extensions;
using ModularMonolith.Api.ConfigurationOptions;
using ModularMonolith.Identity.Extensions;
using ModularMonolith.Notification.Extensions;
using ModularMonolith.Preference.Extensions;
using ModularMonolith.Profile.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services.Configure<AppSettings>(configuration);

builder.AddIdentityModules(opt => configuration.GetSection("Modules:Identity").Bind(opt));
builder.AddNotificationModules((opt) => configuration.GetSection("Modules:Notification").Bind(opt));
builder.AddProfileModules((opt) => configuration.GetSection("Modules:Profile").Bind(opt));
builder.AddPreferenceModules((opt) => configuration.GetSection("Modules:Preference").Bind(opt));

builder.AddApplicationSevices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityModules();
app.UseNotificationModules();
app.UseProfileModules();
app.UsePreferenceModules();
app.UseApplicationServices();

app.Run();
