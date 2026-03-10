dotnet ef add migrations initial -Context NotificationDbContext -Project Notification -StartupProject Api -o Infrastructure\Migrations

dotnet ef database update -Context NotificationDbContext
