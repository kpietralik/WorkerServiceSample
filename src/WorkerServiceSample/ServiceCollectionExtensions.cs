using Microsoft.Extensions.DependencyInjection;
using System;

namespace WorkerServiceSample
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIpfyClient(this IServiceCollection services)
        {
            services
                .AddHttpClient<IpfyClient>()
                .SetHandlerLifetime(TimeSpan.FromHours(24));

            return services;
        }

        public static IServiceCollection AddBlobContainerClient(this IServiceCollection services)
        {
            services
                .AddHttpClient<BlobContainerClient>()
                .SetHandlerLifetime(TimeSpan.FromHours(1));

            return services;
        }
    }
}
