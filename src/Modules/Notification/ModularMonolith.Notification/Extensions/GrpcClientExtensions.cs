using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Preference.Grpc.Services;

namespace ModularMonolith.Notification.Extensions;

public static class GrpcClientExtensions
{
    public static IServiceCollection AddCustomGrpcClient(this IServiceCollection services)
    {
        // don't config: grpc service = lifetime scoped
        // config: grpc client = lifetime transient

        services.AddGrpcClient<PreferenceGrpcService.PreferenceGrpcServiceClient>(o =>
        {
            o.Address = new Uri("https://localhost:7265");
        });
        return services;
    }
}
