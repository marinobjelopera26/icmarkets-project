using Microsoft.Extensions.DependencyInjection;

namespace ICM.Crypto.Infrastructure.BlockCypher;

public static class DependencyInjection
{
    public static IServiceCollection AddBlockCypher(this IServiceCollection services)
    {
        return services;
    }
}