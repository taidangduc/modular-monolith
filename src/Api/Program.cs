using Api.Extensions;
using ModularMonolith.Identity.Extensions;
using ModularMonolith.Notification.Extensions;
using ModularMonolith.Preference.Extensions;
using ModularMonolith.Profile.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentityModules();
builder.AddNotificationModules();
builder.AddProfileModules();
builder.AddPreferenceModules();
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
