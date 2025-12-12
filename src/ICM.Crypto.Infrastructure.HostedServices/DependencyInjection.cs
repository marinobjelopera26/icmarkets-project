using ICM.Crypto.Infrastructure.HostedServices.DataIngestion;
using ICM.Crypto.Infrastructure.HostedServices.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Infrastructure.HostedServices;

public static class DependencyInjection
{
    public static IServiceCollection AddDataIngestionHostedService(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataIngestionOptions>(
            configuration.GetSection(DataIngestionOptions.SectionName));

        services.AddHostedService<BlockchainSnapshotDataIngestionService>();
        
        return services;
    }
}