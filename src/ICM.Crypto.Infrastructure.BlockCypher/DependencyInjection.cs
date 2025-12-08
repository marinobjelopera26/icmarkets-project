using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Infrastructure.BlockCypher;

public static class DependencyInjection
{
    public static IServiceCollection AddBlockCypher(this IServiceCollection services)
    {
        services.AddHttpClient<IBlockCypherService, BlockCypherService>(
            client =>
            {
                client.BaseAddress = new Uri("https://api.blockcypher.com/v1/");
                client.Timeout = TimeSpan.FromSeconds(15);
            });

        return services;
    }
}